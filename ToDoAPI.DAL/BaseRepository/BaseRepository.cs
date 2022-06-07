using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ToDoAPI.DAL.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public IQueryable<T> FindAll() => _dbSet.AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _dbSet.Where(expression).AsNoTracking();
        public void Create(T entity) => _dbSet.Add(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);


        public async Task<T> GetAync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        //public async Task<bool> ExistsAsync(int id)
        //{
        //    var entity = await GetAync(id);
        //    return entity != null;
        //}
    }
}
