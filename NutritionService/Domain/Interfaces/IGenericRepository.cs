using NutritionService.Domain.Models;

namespace NutritionService.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        public void SaveInclude(TEntity entity, params string[] includedProperties);
        IQueryable<TEntity> GetAllAsync(bool trackChanges = false);
        Task<TEntity?> GetByIdAsync(int id);


    }
}
