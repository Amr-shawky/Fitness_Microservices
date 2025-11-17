using NutritionService.Domain.Models;

namespace NutritionService.Domain.Interfaces
{
    public interface IUnitOfWork
    {

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        Task<int> SaveChangesAsync();
    }
}
