using NutritionService.Domain.Models;
using System.Linq.Expressions;

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
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? criteria = null);


    }
}
