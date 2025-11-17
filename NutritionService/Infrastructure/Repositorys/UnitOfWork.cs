using NutritionService.Domain.Interfaces;
using NutritionService.Domain.Models;
using NutritionService.Infrastructure.Data;
using System.Collections.Concurrent;

namespace NutritionService.Infrastructure.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _Repositories;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _Repositories = new ConcurrentDictionary<string, object>();
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            return (IGenericRepository<TEntity>)_Repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity>(_dbContext));
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
