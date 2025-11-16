using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Domain.Entities;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features.Shared;
using WorkoutService.Features.Workouts.GetWorkoutDetails.ViewModels;

namespace WorkoutService.Features.Workouts.GetWorkoutDetails
{
    public class GetWorkoutDetailsHandler : IRequestHandler<GetWorkoutDetailsQuery, RequestResponse<WorkoutDetailsViewModel>>
    {
        private readonly IBaseRepository<Workout> _workoutRepository;

        public GetWorkoutDetailsHandler(IBaseRepository<Workout> workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<RequestResponse<WorkoutDetailsViewModel>> Handle(GetWorkoutDetailsQuery request, CancellationToken cancellationToken)
        {
            var workout = await _workoutRepository.GetAll()
                .Include(w => w.WorkoutExercises)
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (workout == null)
            {
                return RequestResponse<WorkoutDetailsViewModel>.Fail("Workout not found");
            }

            var workoutDetailsVm = workout.Adapt<WorkoutDetailsViewModel>();

            // Mocking variations and tips for now, as they are not in the domain model
            workoutDetailsVm.Variations = new WorkoutVariationsViewModel
            {
                Beginner = new VariationViewModel { Modifications = new List<string> { "Knee push-ups" }, EstimatedDuration = 35, CaloriesBurn = 250 },
                Advanced = new VariationViewModel { Modifications = new List<string> { "Weighted push-ups" }, EstimatedDuration = 55, CaloriesBurn = 450 }
            };
            workoutDetailsVm.Tips = new List<string> { "Warm up properly", "Focus on form" };

            return RequestResponse<WorkoutDetailsViewModel>.Success(workoutDetailsVm, "Workout details fetched successfully");
        }
    }
}
