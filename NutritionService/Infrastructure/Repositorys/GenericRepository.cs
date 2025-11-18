using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public void  Create(TEntity entity)
          =>  _context.Set<TEntity>().Add(entity);

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.Now;
            _context.Set<TEntity>().Update(entity);
        }
        public void SaveInclude(TEntity entity, params string[] includedProperties)
        {
            var LocalEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
            EntityEntry entry;

            if (LocalEntity == null)
            {
                entry = _context.Entry(entity);
            }
            else
            {
                entry = _context.ChangeTracker.Entries<TEntity>().First(e => e.Entity.Id == entity.Id);
            }

            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                    continue;
                else
                {
                    if (includedProperties.Contains(property.Metadata.Name))
                    {
                        property.IsModified = true;
                    }
                    else
                    {
                        property.IsModified = false;
                    }
                }
            }
        }
        public IQueryable<TEntity> GetAllAsync(bool trackChanges = false)
        {
            var query = _context.Set<TEntity>()
               .Where(e => !e.IsDeleted)
               .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
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
