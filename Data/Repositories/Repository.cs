using Application.Gateways.DataAccess;
using Data;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly HackatonDbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        protected Repository(HackatonDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }
        public void Dispose() => _dbContext.Dispose();
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.AsNoTracking().ToArrayAsync();
        public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task RemoveAsync(int id)
        {
            _dbSet.Remove(await GetByIdAsync(id));
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
