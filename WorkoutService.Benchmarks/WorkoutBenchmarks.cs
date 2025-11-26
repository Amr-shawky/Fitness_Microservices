using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Domain.Entities;
using WorkoutService.Infrastructure.Data;
using WorkoutService.Infrastructure;
using WorkoutService.Features.Workouts.GetWorkoutDetails.ViewModels;
using Mapster;

namespace WorkoutService.Benchmarks
{
    [MemoryDiagnoser]
    public class WorkoutBenchmarks
    {
        private ApplicationDbContext _context;
        private BaseRepository<Workout> _repository;
        private int _workoutId;

        [GlobalSetup]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new BaseRepository<Workout>(_context);

            // Seed Data
            var workout = new Workout
            {
                Name = "Benchmark Workout",
                Description = "Testing performance",
                Category = "Test",
                Difficulty = "Hard",
                WorkoutExercises = new List<WorkoutExercise>()
            };

            for (int i = 0; i < 100; i++)
            {
                workout.WorkoutExercises.Add(new WorkoutExercise
                {
                    Order = i,
                    Sets = 3,
                    Reps = "10",
                    Exercise = new Exercise
                    {
                        Name = $"Exercise {i}",
                        Description = "Desc",
                        TargetMuscles = new List<string> { "Chest" },
                        EquipmentNeeded = new List<string> { "None" }
                    }
                });
            }

            _context.Workouts.Add(workout);
            _context.SaveChanges();
            _workoutId = workout.Id;
        }

        [Benchmark(Baseline = true)]
        public async Task<WorkoutDetailsViewModel> GetWorkout_WithInclude()
        {
            // Simulating the OLD way (Inefficient)
            var workout = await _repository.GetAll()
                .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Exercise)
                .FirstOrDefaultAsync(w => w.Id == _workoutId);

            return workout.Adapt<WorkoutDetailsViewModel>();
        }

        [Benchmark]
        public async Task<WorkoutDetailsViewModel> GetWorkout_WithProjection()
        {
            // Simulating the NEW way (Optimized)
            return await _repository.GetAll()
                .Where(w => w.Id == _workoutId)
                .Select(w => new WorkoutDetailsViewModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description,
                    Category = w.Category,
                    Difficulty = w.Difficulty,
                    DurationInMinutes = w.DurationInMinutes,
                    CaloriesBurn = w.CaloriesBurn,
                    IsPremium = w.IsPremium,
                    Rating = w.Rating,
                    Exercises = w.WorkoutExercises.Select(we => new WorkoutExerciseViewModel
                    {
                        ExerciseId = we.ExerciseId,
                        Name = we.Exercise.Name,
                        Sets = we.Sets,
                        Reps = we.Reps,
                        RestTimeInSeconds = we.RestTimeInSeconds,
                        Order = we.Order,
                        TargetMuscles = we.Exercise.TargetMuscles,
                        EquipmentNeeded = we.Exercise.EquipmentNeeded
                    }).OrderBy(e => e.Order).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
