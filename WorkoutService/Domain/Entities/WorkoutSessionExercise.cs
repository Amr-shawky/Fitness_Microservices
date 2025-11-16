using System;

namespace WorkoutService.Domain.Entities
{
    public class WorkoutSessionExercise : BaseEntity
    {
        public Guid WorkoutSessionId { get; set; }
        public WorkoutSession WorkoutSession { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Completed"
        public int Order { get; set; }
    }
}
