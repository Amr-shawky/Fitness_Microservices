using FluentValidation;

namespace WorkoutService.Features.Workouts.CreateWorkout
{
    public class CreateWorkoutValidator : AbstractValidator<CreateWorkoutDto>
    {
        public CreateWorkoutValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
