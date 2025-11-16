using Microsoft.EntityFrameworkCore;
using WorkoutService.Domain.Entities; // Your correct namespace

namespace WorkoutService.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider sp)
        {
            var ctx = sp.GetRequiredService<ApplicationDbContext>();

            await SeedWorkoutPlansAsync(ctx);
            await SeedExercisesAsync(ctx);
            await SeedWorkoutsAsync(ctx);
        }

        private static async Task SeedWorkoutPlansAsync(ApplicationDbContext ctx)
        {
            if (await ctx.WorkoutPlans.AnyAsync()) return;

            var plans = new List<WorkoutPlan>
            {
                new() {
                    ExternalPlanId = "plan_lw_normal",
                    Name = "Weight Loss - Normal Intensity",
                    Description = "A balanced plan for steady weight loss.",
                    Goal = "Lose Weight",
                    Status = "Normal",
                    Difficulty = "Intermediate"
                },
                new() {
                    ExternalPlanId = "plan_gw_hard",
                    Name = "Gain Weight - Hard Intensity",
                    Description = "A high-volume plan for building mass.",
                    Goal = "Gain Weight",
                    Status = "Hard",
                    Difficulty = "Advanced"
                },
                new() {
                    ExternalPlanId = "plan_fit_beginner",
                    Name = "Get Fitter - Beginner",
                    Description = "An introduction to fitness fundamentals.",
                    Goal = "Get Fitter",
                    Status = "Normal",
                    Difficulty = "Beginner"
                },
                new() {
                    ExternalPlanId = "plan_fit_intermediate",
                    Name = "Get Fitter - Intermediate",
                    Description = "Step up your fitness with more challenging routines.",
                    Goal = "Get Fitter",
                    Status = "Normal",
                    Difficulty = "Intermediate"
                },
                new() {
                    ExternalPlanId = "plan_fit_advanced",
                    Name = "Get Fitter - Advanced",
                    Description = "Push your limits with high-intensity workouts.",
                    Goal = "Get Fitter",
                    Status = "Hard",
                    Difficulty = "Advanced"
                },
                new() {
                    ExternalPlanId = "plan_lw_easy",
                    Name = "Weight Loss - Easy Start",
                    Description = "A gentle introduction to weight loss exercises.",
                    Goal = "Lose Weight",
                    Status = "Easy",
                    Difficulty = "Beginner"
                },
                new() {
                    ExternalPlanId = "plan_lw_hard",
                    Name = "Weight Loss - High Intensity",
                    Description = "Intense cardio and strength training for maximum fat burn.",
                    Goal = "Lose Weight",
                    Status = "Hard",
                    Difficulty = "Advanced"
                },
                new() {
                    ExternalPlanId = "plan_gw_beginner",
                    Name = "Gain Weight - Beginner Bulk",
                    Description = "Fundamental strength exercises to start building muscle.",
                    Goal = "Gain Weight",
                    Status = "Normal",
                    Difficulty = "Beginner"
                },
                new() {
                    ExternalPlanId = "plan_gw_intermediate",
                    Name = "Gain Weight - Intermediate Mass",
                    Description = "Increase volume and intensity to pack on more size.",
                    Goal = "Gain Weight",
                    Status = "Normal",
                    Difficulty = "Intermediate"
                },
                new() {
                    ExternalPlanId = "plan_st_beginner",
                    Name = "Strength Training - Beginner",
                    Description = "Learn the core lifts and build a solid strength base.",
                    Goal = "Build Strength",
                    Status = "Normal",
                    Difficulty = "Beginner"
                },
                new() {
                    ExternalPlanId = "plan_st_intermediate",
                    Name = "Strength Training - Intermediate",
                    Description = "Intermediate programming for consistent strength gains.",
                    Goal = "Build Strength",
                    Status = "Normal",
                    Difficulty = "Intermediate"
                },
                new() {
                    ExternalPlanId = "plan_st_advanced",
                    Name = "Strength Training - Advanced Power",
                    Description = "Advanced techniques for experienced lifters.",
                    Goal = "Build Strength",
                    Status = "Hard",
                    Difficulty = "Advanced"
                },
                new() {
                    ExternalPlanId = "plan_flex_beginner",
                    Name = "Flexibility - Beginner Yoga",
                    Description = "Basic yoga poses to improve flexibility and reduce stress.",
                    Goal = "Improve Flexibility",
                    Status = "Easy",
                    Difficulty = "Beginner"
                },
                new() {
                    ExternalPlanId = "plan_flex_intermediate",
                    Name = "Flexibility - Dynamic Stretching",
                    Description = "Improve mobility with dynamic stretches and movements.",
                    Goal = "Improve Flexibility",
                    Status = "Normal",
                    Difficulty = "Intermediate"
                },
                new() {
                    ExternalPlanId = "plan_end_beginner",
                    Name = "Endurance - Cardio Starter",
                    Description = "Build your cardiovascular base with steady-state cardio.",
                    Goal = "Improve Endurance",
                    Status = "Easy",
                    Difficulty = "Beginner"
                },
                new() {
                    ExternalPlanId = "plan_end_intermediate",
                    Name = "Endurance - HIIT Cardio",
                    Description = "High-Intensity Interval Training to boost endurance and burn calories.",
                    Goal = "Improve Endurance",
                    Status = "Hard",
                    Difficulty = "Intermediate"
                },
                new() {
                    ExternalPlanId = "plan_end_advanced",
                    Name = "Endurance - Marathon Prep",
                    Description = "Advanced cardio training for long-distance events.",
                    Goal = "Improve Endurance",
                    Status = "Hard",
                    Difficulty = "Advanced"
                },
                new() {
                    ExternalPlanId = "plan_body_beginner",
                    Name = "Bodyweight - Beginner Basics",
                    Description = "Master the fundamentals of bodyweight training.",
                    Goal = "Bodyweight Fitness",
                    Status = "Normal",
                    Difficulty = "Beginner"
                },
                new() {
                    ExternalPlanId = "plan_body_advanced",
                    Name = "Bodyweight - Calisthenics Master",
                    Description = "Advanced calisthenics skills and progressions.",
                    Goal = "Bodyweight Fitness",
                    Status = "Hard",
                    Difficulty = "Advanced"
                },
                new() {
                    ExternalPlanId = "plan_mind_beginner",
                    Name = "Mind & Body - Meditation",
                    Description = "Guided meditation for focus and stress relief.",
                    Goal = "Mindfulness",
                    Status = "Easy",
                    Difficulty = "Beginner"
                },
                 new() {
                    ExternalPlanId = "plan_rec_active",
                    Name = "Active Recovery",
                    Description = "Low-intensity workouts to aid recovery and reduce soreness.",
                    Goal = "Recovery",
                    Status = "Easy",
                    Difficulty = "Beginner"
                }
            };

            await ctx.WorkoutPlans.AddRangeAsync(plans);
            await ctx.SaveChangesAsync();
        }

        private static async Task SeedExercisesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.Exercises.AnyAsync()) return;

            var exercises = new List<Exercise>
            {
                new() {
                    Name = "Push-up",
                    Description = "A basic calisthenic exercise for upper body strength.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Chest", "Triceps", "Shoulders" },
                    EquipmentNeeded = new List<string> { "Bodyweight" }
                },
                new() {
                    Name = "Bodyweight Squat",
                    Description = "A fundamental lower body exercise.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Quads", "Glutes", "Hamstrings" },
                    EquipmentNeeded = new List<string> { "Bodyweight" }
                },
                new() {
                    Name = "Plank",
                    Description = "An isometric core strength exercise.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Core", "Abs" },
                    EquipmentNeeded = new List<string> { "Bodyweight" }
                },
                new() {
                    Name = "Dumbbell Bench Press",
                    Description = "A chest-building exercise using dumbbells.",
                    Difficulty = "Intermediate",
                    TargetMuscles = new List<string> { "Chest", "Triceps" },
                    EquipmentNeeded = new List<string> { "Dumbbells", "Bench" }
                },
                new() {
                    Name = "Dumbbell Row",
                    Description = "A back-building exercise using a single dumbbell.",
                    Difficulty = "Intermediate",
                    TargetMuscles = new List<string> { "Back", "Lats", "Biceps" },
                    EquipmentNeeded = new List<string> { "Dumbbells", "Bench" }
                },
                // Chest Exercises
                new() {
                    Name = "Incline Dumbbell Press",
                    Description = "Targets the upper chest muscles.",
                    Difficulty = "Intermediate",
                    TargetMuscles = new List<string> { "Chest", "Shoulders", "Triceps" },
                    EquipmentNeeded = new List<string> { "Dumbbells", "Incline Bench" }
                },
                new() {
                    Name = "Barbell Bench Press",
                    Description = "The classic lift for chest, shoulders, and triceps.",
                    Difficulty = "Intermediate",
                    TargetMuscles = new List<string> { "Chest", "Shoulders", "Triceps" },
                    EquipmentNeeded = new List<string> { "Barbell", "Bench" }
                },
                new() {
                    Name = "Cable Crossover",
                    Description = "An isolation exercise for the chest.",
                    Difficulty = "Intermediate",
                    TargetMuscles = new List<string> { "Chest" },
                    EquipmentNeeded = new List<string> { "Cable Machine" }
                },
                // Back Exercises
                new() {
                    Name = "Pull-up",
                    Description = "A challenging bodyweight exercise for the back and biceps.",
                    Difficulty = "Advanced",
                    TargetMuscles = new List<string> { "Back", "Lats", "Biceps" },
                    EquipmentNeeded = new List<string> { "Pull-up Bar" }
                },
                new() {
                    Name = "Lat Pulldown",
                    Description = "A machine-based alternative to pull-ups.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Back", "Lats" },
                    EquipmentNeeded = new List<string> { "Lat Pulldown Machine" }
                },
                new() {
                    Name = "Deadlift",
                    Description = "A full-body lift that heavily targets the back and legs.",
                    Difficulty = "Advanced",
                    TargetMuscles = new List<string> { "Back", "Glutes", "Hamstrings" },
                    EquipmentNeeded = new List<string> { "Barbell" }
                },
                // Leg Exercises
                new() {
                    Name = "Barbell Squat",
                    Description = "The king of leg exercises, targeting the entire lower body.",
                    Difficulty = "Advanced",
                    TargetMuscles = new List<string> { "Quads", "Glutes", "Hamstrings" },
                    EquipmentNeeded = new List<string> { "Barbell", "Squat Rack" }
                },
                new() {
                    Name = "Leg Press",
                    Description = "A machine-based exercise for lower body strength.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Quads", "Glutes" },
                    EquipmentNeeded = new List<string> { "Leg Press Machine" }
                },
                new() {
                    Name = "Lunge",
                    Description = "A unilateral exercise for balance and leg strength.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Quads", "Glutes" },
                    EquipmentNeeded = new List<string> { "Bodyweight", "Dumbbells" }
                },
                new() {
                    Name = "Romanian Deadlift",
                    Description = "Targets the hamstrings and glutes.",
                    Difficulty = "Intermediate",
                    TargetMuscles = new List<string> { "Hamstrings", "Glutes" },
                    EquipmentNeeded = new List<string> { "Barbell", "Dumbbells" }
                },
                 new() {
                    Name = "Calf Raise",
                    Description = "An isolation exercise for the calf muscles.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Calves" },
                    EquipmentNeeded = new List<string> { "Bodyweight", "Dumbbells" }
                },
                // Shoulder Exercises
                new() {
                    Name = "Overhead Press",
                    Description = "A compound movement for shoulder strength.",
                    Difficulty = "Intermediate",
                    TargetMuscles = new List<string> { "Shoulders", "Triceps" },
                    EquipmentNeeded = new List<string> { "Barbell", "Dumbbells" }
                },
                new() {
                    Name = "Lateral Raise",
                    Description = "Isolates the side deltoids for broader shoulders.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Shoulders" },
                    EquipmentNeeded = new List<string> { "Dumbbells" }
                },
                new() {
                    Name = "Face Pull",
                    Description = "Excellent for shoulder health and rear deltoid development.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Shoulders", "Upper Back" },
                    EquipmentNeeded = new List<string> { "Cable Machine", "Resistance Bands" }
                },
                // Arm Exercises
                new() {
                    Name = "Bicep Curl",
                    Description = "The classic exercise for building biceps.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Biceps" },
                    EquipmentNeeded = new List<string> { "Dumbbells", "Barbell" }
                },
                new() {
                    Name = "Tricep Pushdown",
                    Description = "Isolates the triceps using a cable machine.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Triceps" },
                    EquipmentNeeded = new List<string> { "Cable Machine" }
                },
                new() {
                    Name = "Skull Crusher",
                    Description = "An effective exercise for tricep mass.",
                    Difficulty = "Intermediate",
                    TargetMuscles = new List<string> { "Triceps" },
                    EquipmentNeeded = new List<string> { "EZ Bar", "Dumbbells", "Bench" }
                },
                new() {
                    Name = "Hammer Curl",
                    Description = "Targets the biceps and brachialis for thicker arms.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Biceps", "Forearms" },
                    EquipmentNeeded = new List<string> { "Dumbbells" }
                },
                // Core Exercises
                new() {
                    Name = "Leg Raise",
                    Description = "Targets the lower abs.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Abs", "Core" },
                    EquipmentNeeded = new List<string> { "Bodyweight" }
                },
                new() {
                    Name = "Russian Twist",
                    Description = "Works the obliques and overall core stability.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Abs", "Obliques", "Core" },
                    EquipmentNeeded = new List<string> { "Bodyweight", "Kettlebell", "Dumbbell" }
                },
                new() {
                    Name = "Ab Wheel Rollout",
                    Description = "An advanced core exercise for serious abdominal strength.",
                    Difficulty = "Advanced",
                    TargetMuscles = new List<string> { "Abs", "Core", "Shoulders" },
                    EquipmentNeeded = new List<string> { "Ab Wheel" }
                },
                // Cardio
                new() {
                    Name = "Running",
                    Description = "Classic cardiovascular exercise.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Cardio" },
                    EquipmentNeeded = new List<string> { "None" }
                },
                new() {
                    Name = "Jumping Jacks",
                    Description = "A full-body cardio warm-up.",
                    Difficulty = "Beginner",
                    TargetMuscles = new List<string> { "Cardio" },
                    EquipmentNeeded = new List<string> { "Bodyweight" }
                },
                new() {
                    Name = "Burpee",
                    Description = "A high-intensity, full-body cardio and strength exercise.",
                    Difficulty = "Intermediate",
                    TargetMuscles = new List<string> { "Cardio", "Full Body" },
                    EquipmentNeeded = new List<string> { "Bodyweight" }
                }
            };

            await ctx.Exercises.AddRangeAsync(exercises);
            await ctx.SaveChangesAsync();
        }

        private static async Task SeedWorkoutsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.Workouts.AnyAsync()) return;

            var planBeginner = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_fit_beginner");
            var planNormal = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_lw_normal");

            var pushup = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Push-up");
            var squat = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Bodyweight Squat");
            var plank = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Plank");
            var dbPress = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Dumbbell Bench Press");
            var dbRow = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Dumbbell Row");

            var planFitIntermediate = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_fit_intermediate");
            var planFitAdvanced = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_fit_advanced");
            var planLwEasy = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_lw_easy");
            var planLwHard = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_lw_hard");
            var planGwBeginner = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_gw_beginner");
            var planStAdvanced = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_st_advanced");
            var planFlexBeginner = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_flex_beginner");
            var planEndIntermediate = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_end_intermediate");
            var planBodyAdvanced = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_body_advanced");
            var planRecActive = await ctx.WorkoutPlans.FirstOrDefaultAsync(p => p.ExternalPlanId == "plan_rec_active");


            var inclineDbPress = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Incline Dumbbell Press");
            var barbellBench = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Barbell Bench Press");
            var cableCrossover = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Cable Crossover");
            var pullup = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Pull-up");
            var latPulldown = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Lat Pulldown");
            var deadlift = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Deadlift");
            var barbellSquat = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Barbell Squat");
            var legPress = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Leg Press");
            var lunge = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Lunge");
            var rdl = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Romanian Deadlift");
            var calfRaise = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Calf Raise");
            var overheadPress = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Overhead Press");
            var lateralRaise = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Lateral Raise");
            var facePull = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Face Pull");
            var bicepCurl = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Bicep Curl");
            var tricepPushdown = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Tricep Pushdown");
            var skullCrusher = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Skull Crusher");
            var hammerCurl = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Hammer Curl");
            var legRaise = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Leg Raise");
            var russianTwist = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Russian Twist");
            var abWheel = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Ab Wheel Rollout");
            var running = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Running");
            var jumpingJacks = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Jumping Jacks");
            var burpee = await ctx.Exercises.FirstOrDefaultAsync(e => e.Name == "Burpee");

            if (planBeginner == null || planNormal == null || pushup == null || squat == null || plank == null || dbPress == null || dbRow == null ||
                planFitIntermediate == null || planFitAdvanced == null || planLwEasy == null || planLwHard == null || planGwBeginner == null ||
                planStAdvanced == null || planFlexBeginner == null || planEndIntermediate == null || planBodyAdvanced == null || planRecActive == null ||
                inclineDbPress == null || barbellBench == null || cableCrossover == null || pullup == null || latPulldown == null || deadlift == null ||
                barbellSquat == null || legPress == null || lunge == null || rdl == null || calfRaise == null || overheadPress == null ||
                lateralRaise == null || facePull == null || bicepCurl == null || tricepPushdown == null || skullCrusher == null || hammerCurl == null ||
                legRaise == null || russianTwist == null || abWheel == null || running == null || jumpingJacks == null || burpee == null)
            {
                return; // Can't seed
            }

            var workouts = new List<Workout>
            {
                new() {
                    Name = "Full Body Introduction",
                    Description = "A workout to learn the basic movements.",
                    Category = "full-body",
                    Difficulty = "Beginner",
                    DurationInMinutes = 20,
                    CaloriesBurn = 150,
                    IsPremium = false,
                    Rating = 0,
                    WorkoutPlan = planBeginner, // --- FIX: Use navigation property
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = squat, Order = 1, Sets = 3, Reps = "8-12", RestTimeInSeconds = 60 },
                        new() { Exercise = pushup, Order = 2, Sets = 3, Reps = "5-10 (Knees OK)", RestTimeInSeconds = 60 },
                        new() { Exercise = plank, Order = 3, Sets = 3, Reps = "30s", RestTimeInSeconds = 45 }
                    }
                },
                new() {
                    Name = "Strength & Cardio Mix",
                    Description = "A routine to burn calories and build muscle.",
                    Category = "full-body",
                    Difficulty = "Intermediate",
                    DurationInMinutes = 45,
                    CaloriesBurn = 350,
                    IsPremium = false,
                    Rating = 0,
                    WorkoutPlan = planNormal, // --- FIX: Use navigation property
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = squat, Order = 1, Sets = 3, Reps = "12-15", RestTimeInSeconds = 60 },
                        new() { Exercise = dbPress, Order = 2, Sets = 3, Reps = "10-12", RestTimeInSeconds = 60 },
                        new() { Exercise = dbRow, Order = 3, Sets = 3, Reps = "10-12 (each side)", RestTimeInSeconds = 60 },
                        new() { Exercise = plank, Order = 4, Sets = 3, Reps = "60s", RestTimeInSeconds = 45 }
                    }
                },
                new() {
                    Name = "Chest Annihilation",
                    Description = "A high-volume workout for serious chest development.",
                    Category = "chest",
                    Difficulty = "Advanced",
                    DurationInMinutes = 60,
                    CaloriesBurn = 450,
                    IsPremium = true,
                    Rating = 5,
                    WorkoutPlan = planStAdvanced,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = barbellBench, Order = 1, Sets = 4, Reps = "6-8", RestTimeInSeconds = 90 },
                        new() { Exercise = inclineDbPress, Order = 2, Sets = 4, Reps = "8-10", RestTimeInSeconds = 75 },
                        new() { Exercise = cableCrossover, Order = 3, Sets = 3, Reps = "12-15", RestTimeInSeconds = 60 },
                        new() { Exercise = pushup, Order = 4, Sets = 3, Reps = "To Failure", RestTimeInSeconds = 60 }
                    }
                },
                new() {
                    Name = "Back Builder",
                    Description = "Develop a wide and thick back with these essential exercises.",
                    Category = "back",
                    Difficulty = "Intermediate",
                    DurationInMinutes = 50,
                    CaloriesBurn = 400,
                    IsPremium = false,
                    Rating = 4.5,
                    WorkoutPlan = planFitIntermediate,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = pullup, Order = 1, Sets = 4, Reps = "As many as possible", RestTimeInSeconds = 90 },
                        new() { Exercise = dbRow, Order = 2, Sets = 3, Reps = "8-12 (each side)", RestTimeInSeconds = 75 },
                        new() { Exercise = latPulldown, Order = 3, Sets = 3, Reps = "10-12", RestTimeInSeconds = 60 },
                        new() { Exercise = facePull, Order = 4, Sets = 3, Reps = "15-20", RestTimeInSeconds = 45 }
                    }
                },
                new() {
                    Name = "Leg Day Burnout",
                    Description = "A challenging leg workout that will leave you sore for days.",
                    Category = "legs",
                    Difficulty = "Advanced",
                    DurationInMinutes = 75,
                    CaloriesBurn = 600,
                    IsPremium = true,
                    Rating = 5,
                    WorkoutPlan = planLwHard,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = barbellSquat, Order = 1, Sets = 5, Reps = "5", RestTimeInSeconds = 120 },
                        new() { Exercise = legPress, Order = 2, Sets = 4, Reps = "10-12", RestTimeInSeconds = 90 },
                        new() { Exercise = rdl, Order = 3, Sets = 3, Reps = "8-10", RestTimeInSeconds = 75 },
                        new() { Exercise = lunge, Order = 4, Sets = 3, Reps = "12 (each leg)", RestTimeInSeconds = 60 },
                        new() { Exercise = calfRaise, Order = 5, Sets = 4, Reps = "15-20", RestTimeInSeconds = 45 }
                    }
                },
                new() {
                    Name = "Shoulder Sculpt",
                    Description = "Build strong, rounded shoulders.",
                    Category = "shoulders",
                    Difficulty = "Intermediate",
                    DurationInMinutes = 40,
                    CaloriesBurn = 300,
                    IsPremium = false,
                    Rating = 4,
                    WorkoutPlan = planFitIntermediate,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = overheadPress, Order = 1, Sets = 4, Reps = "8-10", RestTimeInSeconds = 75 },
                        new() { Exercise = lateralRaise, Order = 2, Sets = 3, Reps = "12-15", RestTimeInSeconds = 60 },
                        new() { Exercise = facePull, Order = 3, Sets = 3, Reps = "15-20", RestTimeInSeconds = 45 }
                    }
                },
                new() {
                    Name = "Arm Blaster",
                    Description = "A workout focused on building bigger biceps and triceps.",
                    Category = "arms",
                    Difficulty = "Intermediate",
                    DurationInMinutes = 35,
                    CaloriesBurn = 250,
                    IsPremium = false,
                    Rating = 4.2,
                    WorkoutPlan = planGwBeginner,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = bicepCurl, Order = 1, Sets = 3, Reps = "10-12", RestTimeInSeconds = 60 },
                        new() { Exercise = tricepPushdown, Order = 2, Sets = 3, Reps = "10-12", RestTimeInSeconds = 60 },
                        new() { Exercise = hammerCurl, Order = 3, Sets = 3, Reps = "10-12", RestTimeInSeconds = 60 },
                        new() { Exercise = skullCrusher, Order = 4, Sets = 3, Reps = "10-12", RestTimeInSeconds = 75 }
                    }
                },
                new() {
                    Name = "Core Crusher",
                    Description = "A quick and intense workout to strengthen your core.",
                    Category = "stomach",
                    Difficulty = "Intermediate",
                    DurationInMinutes = 15,
                    CaloriesBurn = 100,
                    IsPremium = false,
                    Rating = 4.8,
                    WorkoutPlan = planFitIntermediate,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = legRaise, Order = 1, Sets = 3, Reps = "15-20", RestTimeInSeconds = 45 },
                        new() { Exercise = russianTwist, Order = 2, Sets = 3, Reps = "20 (each side)", RestTimeInSeconds = 45 },
                        new() { Exercise = plank, Order = 3, Sets = 3, Reps = "60s hold", RestTimeInSeconds = 60 }
                    }
                },
                new() {
                    Name = "HIIT Cardio Blast",
                    Description = "High-Intensity Interval Training to torch calories and boost metabolism.",
                    Category = "full-body",
                    Difficulty = "Intermediate",
                    DurationInMinutes = 20,
                    CaloriesBurn = 300,
                    IsPremium = true,
                    Rating = 4.9,
                    WorkoutPlan = planEndIntermediate,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = jumpingJacks, Order = 1, Sets = 1, Reps = "60s (Warm-up)", RestTimeInSeconds = 0 },
                        new() { Exercise = burpee, Order = 2, Sets = 4, Reps = "30s on, 30s off", RestTimeInSeconds = 30 },
                        new() { Exercise = squat, Order = 3, Sets = 4, Reps = "30s on, 30s off", RestTimeInSeconds = 30 },
                        new() { Exercise = pushup, Order = 4, Sets = 4, Reps = "30s on, 30s off", RestTimeInSeconds = 30 }
                    }
                },
                new() {
                    Name = "Beginner Bodyweight Circuit",
                    Description = "A simple circuit to get started with bodyweight exercises.",
                    Category = "full-body",
                    Difficulty = "Beginner",
                    DurationInMinutes = 25,
                    CaloriesBurn = 200,
                    IsPremium = false,
                    Rating = 4.3,
                    WorkoutPlan = planBeginner,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = squat, Order = 1, Sets = 3, Reps = "10-15", RestTimeInSeconds = 60 },
                        new() { Exercise = pushup, Order = 2, Sets = 3, Reps = "5-10", RestTimeInSeconds = 60 },
                        new() { Exercise = lunge, Order = 3, Sets = 3, Reps = "10 (each leg)", RestTimeInSeconds = 60 },
                        new() { Exercise = plank, Order = 4, Sets = 3, Reps = "30-45s hold", RestTimeInSeconds = 45 }
                    }
                },
                new() {
                    Name = "Advanced Calisthenics",
                    Description = "Master your body with advanced bodyweight movements.",
                    Category = "full-body",
                    Difficulty = "Advanced",
                    DurationInMinutes = 60,
                    CaloriesBurn = 500,
                    IsPremium = true,
                    Rating = 5,
                    WorkoutPlan = planBodyAdvanced,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = pullup, Order = 1, Sets = 5, Reps = "10-15", RestTimeInSeconds = 90 },
                        new() { Exercise = pushup, Order = 2, Sets = 5, Reps = "20-30", RestTimeInSeconds = 75 },
                        new() { Exercise = abWheel, Order = 3, Sets = 4, Reps = "10-15", RestTimeInSeconds = 60 },
                        new() { Exercise = burpee, Order = 4, Sets = 4, Reps = "15-20", RestTimeInSeconds = 60 }
                    }
                },
                new() {
                    Name = "Gentle Yoga Flow",
                    Description = "A relaxing yoga sequence to improve flexibility and mindfulness.",
                    Category = "flexibility",
                    Difficulty = "Beginner",
                    DurationInMinutes = 30,
                    CaloriesBurn = 100,
                    IsPremium = false,
                    Rating = 4.7,
                    WorkoutPlan = planFlexBeginner,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = plank, Order = 1, Sets = 1, Reps = "Hold for 5 breaths", RestTimeInSeconds = 15 },
                        new() { Exercise = squat, Order = 2, Sets = 1, Reps = "Flow through 10 reps", RestTimeInSeconds = 15 },
                        new() { Exercise = lunge, Order = 3, Sets = 1, Reps = "Hold for 5 breaths each side", RestTimeInSeconds = 15 }
                    }
                },
                new() {
                    Name = "Active Recovery Day",
                    Description = "A light workout to help your muscles recover and grow.",
                    Category = "full-body",
                    Difficulty = "Beginner",
                    DurationInMinutes = 20,
                    CaloriesBurn = 100,
                    IsPremium = false,
                    Rating = 4.6,
                    WorkoutPlan = planRecActive,
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new() { Exercise = running, Order = 1, Sets = 1, Reps = "10 minutes light jog", RestTimeInSeconds = 0 },
                        new() { Exercise = jumpingJacks, Order = 2, Sets = 2, Reps = "30s", RestTimeInSeconds = 30 },
                        new() { Exercise = plank, Order = 3, Sets = 2, Reps = "30s", RestTimeInSeconds = 30 }
                    }
                }
            };

            await ctx.Workouts.AddRangeAsync(workouts);
            await ctx.SaveChangesAsync();
        }
    }
}