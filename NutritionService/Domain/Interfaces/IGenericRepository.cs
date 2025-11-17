using NutritionService.Domain.Models;

namespace NutritionService.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetAllAsync(bool trackChanges = false);
        Task<TEntity?> GetByIdAsync(Guid id);


    }
}
