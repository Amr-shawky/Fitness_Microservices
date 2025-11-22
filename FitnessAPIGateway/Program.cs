using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net.Http; // Required for HttpClientHandler
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Ocelot configuration (loads routes from ocelot.json)
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// ---------------------------------------------------------
// ✅ 1. Register HttpClient with SSL Bypass (For Development Only)
// ---------------------------------------------------------
// This allows the Gateway to talk to self-signed certs in other containers
builder.Services.AddHttpClient("InsecureClient")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) => true // ⚠️ Accept ANY certificate
    });

// 2. Add Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Warning if JWT settings are missing (prevents silent failures)
if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
{
    Console.WriteLine("⚠️ Warning: JWT settings are missing in appsettings.json. Auth might fail.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            // Use key from config or a temp fallback to prevent crash during build
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? "temp_key_for_build_success_only"))
        };
    });

// Add Ocelot services
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// ---------------------------------------------------------
// ✅ 2. Connectivity Test Zone (Minimal APIs)
// ---------------------------------------------------------

// Helper function to test connection
// Now uses IHttpClientFactory to create the insecure client we registered above
async Task<IResult> TestServiceConnection(IHttpClientFactory factory, string serviceName, string url)
{
    try
    {
        // Create the client that ignores SSL errors
        var client = factory.CreateClient("InsecureClient");

        // We try to reach the Swagger UI page as a "Heartbeat" check
        var response = await client.GetAsync(url);

        return Results.Ok(new
        {
            TargetService = serviceName,
            TargetUrl = url,
            StatusCode = response.StatusCode,
            Message = "✅ Success! I can see the service."
        });
    }
    catch (Exception ex)
    {
        return Results.Json(new
        {
            TargetService = serviceName,
            TargetUrl = url,
            Error = ex.Message,
            InnerError = ex.InnerException?.Message,
            Message = "❌ Failed! I cannot reach the service."
        }, statusCode: 500);
    }
}

// 👉 Test Workout Service
app.MapGet("/test/workout", async (IHttpClientFactory factory) =>
{
    // Note: We use the Docker Service Name (workoutservice) and internal HTTPS port (8081)
    // Because WorkoutService redirects HTTP -> HTTPS
    return await TestServiceConnection(factory, "Workout Service", "https://workoutservice:8081/swagger/index.html");
});

// 👉 Test Auth Service
app.MapGet("/test/auth", async (IHttpClientFactory factory) =>
{
    return await TestServiceConnection(factory, "Auth Service", "https://authenticationservice:8081/swagger/index.html");
});

// 👉 Test Nutrition Service
app.MapGet("/test/nutrition", async (IHttpClientFactory factory) =>
{
    return await TestServiceConnection(factory, "Nutrition Service", "https://nutritionservice:8081/swagger/index.html");
});

// ---------------------------------------------------------

// Use Ocelot (Must be the last middleware to handle routed requests)
await app.UseOcelot();

app.Run();