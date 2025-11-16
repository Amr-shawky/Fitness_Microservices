using System;
using System.Collections.Generic;

namespace WorkoutService.Domain.Entities
{
    public class WorkoutSession : BaseEntity
    {
        public Guid UserId { get; set; } // Assuming user ID is a Guid
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public string Status { get; set; } // e.g., "InProgress", "Completed"
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int PlannedDurationInMinutes { get; set; }
        public string Difficulty { get; set; }
        public ICollection<WorkoutSessionExercise> SessionExercises { get; set; } = new List<WorkoutSessionExercise>();
    }
}
