using Microsoft.EntityFrameworkCore;
using NutritionService.Domain.Interfaces;
using NutritionService.Domain.Models;
using NutritionService.Infrastructure.Data;

namespace NutritionService.Infrastructure.Repositorys
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }





        public async Task CreateAsync(TEntity entity)
          => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.Now;
            _context.Set<TEntity>().Update(entity);
        }

        public IQueryable<TEntity> GetAllAsync(bool trackChanges = false)
        {
            var query = _context.Set<TEntity>()
               .Where(e => !e.IsDeleted)
               .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            return entity is not null && !entity.IsDeleted ? entity : null;
        }

        public void Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Set<TEntity>().Update(entity);
        }
    }
}
