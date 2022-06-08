using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ToDoAPI.DAL.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger _logger;
        public BaseRepository(ApplicationDBContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _logger = logger;
        }
        public virtual IQueryable<T> FindAll() => _dbSet.AsNoTracking();
        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _dbSet.Where(expression).AsNoTracking();
        public virtual void Create(T entity) => _dbSet.Add(entity);
        public virtual void Update(T entity)
        {
            _logger.LogInformation("Update Method Called");
            _dbSet.Update(entity);
        }
        public virtual void Delete(T entity)
        {
            _logger.LogInformation("Delete Method Called");
            _dbSet.Remove(entity);
        }


        public virtual async Task<T> GetAync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            _logger.LogInformation("GetAllAsync Method Called");
            return await _dbSet.ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            _logger.LogInformation("AddAsync Method Called");
            await _dbSet.AddAsync(entity);
        }
        public virtual async Task DeleteAsync(int id)
        {
            _logger.LogInformation("DeleteAsync Method Called");
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        //public virtual async Task UpdateAsync(T entity)
        //{
        //    _logger.LogInformation("UpdateAsync Method Called");
        //    //_dbContext.Entry(entity).State = EntityState.Modified;
        //    _dbSet.Update(entity);
        //}

        //public virtual async Task DeleteAsync(T entity)
        //{
        //    _logger.LogInformation("DeleteAsync Method Called");
        //    _dbSet.Remove(entity);
        //}

        //public virtual async Task<bool> ExistsAsync(int id)
        //{
        //    var entity = await GetAync(id);
        //    return entity != null;
        //}
    }
}
