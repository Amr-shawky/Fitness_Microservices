using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Domain.Entities;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features.Shared;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;
using WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels;

namespace WorkoutService.Features.Workouts.GetAllWorkouts
{
    public class GetAllWorkoutsHandler : IRequestHandler<GetAllWorkoutsQuery, RequestResponse<PaginatedWorkoutsVm>>
    {
        private readonly IBaseRepository<Workout> _workoutRepository;

        public GetAllWorkoutsHandler(IBaseRepository<Workout> workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<RequestResponse<PaginatedWorkoutsVm>> Handle(GetAllWorkoutsQuery request, CancellationToken cancellationToken)
        {
            var query = _workoutRepository.GetAll();

            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(w => w.Category == request.Category);
            }

            if (!string.IsNullOrEmpty(request.Difficulty))
            {
                query = query.Where(w => w.Difficulty == request.Difficulty);
            }

            if (request.Duration.HasValue)
            {
                query = query.Where(w => w.DurationInMinutes == request.Duration.Value);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(w => w.Name.Contains(request.Search) || w.Description.Contains(request.Search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var workouts = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var workoutVms = workouts.Adapt<List<WorkoutVm>>();
            var paginatedResult = new PaginatedWorkoutsVm(workoutVms, totalCount, request.Page, request.PageSize);

            return RequestResponse<PaginatedWorkoutsVm>.Success(paginatedResult, "Workouts fetched successfully");
        }
    }
}
