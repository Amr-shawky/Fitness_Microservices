using Mapster;
using MediatR;
using WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.Workouts.GetWorkoutsByCategory
{
    public class GetWorkoutsByCategoryHandler : IRequestHandler<GetWorkoutsByCategoryQuery, PaginatedWorkoutsVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Workout> _workoutRepository;
        public GetWorkoutsByCategoryHandler(IUnitOfWork unitOfWork , IBaseRepository<Workout> workoutRepository)
        {
            _unitOfWork = unitOfWork;
            _workoutRepository = workoutRepository;
        }

        public async Task<PaginatedWorkoutsVm> Handle(GetWorkoutsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var allWorkouts = await _workoutRepository.GetAllAsync();
            var workoutsByCategory = allWorkouts.Where(w => w.Category.Equals(request.Category, StringComparison.OrdinalIgnoreCase));

            var workoutVms = workoutsByCategory.Adapt<List<WorkoutVm>>();

            // Pagination logic (basic example)
            int page =   1;
            int pageSize = workoutVms.Count;
            int totalCount = workoutVms.Count;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var pagedItems = workoutVms.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedWorkoutsVm(pagedItems, page, pageSize, totalCount);
        }
    }
}
