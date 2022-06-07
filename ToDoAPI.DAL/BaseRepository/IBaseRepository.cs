using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.DAL.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T> GetAync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        //Task<bool> ExistsAsync(int id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
