using System.Linq.Expressions;

namespace EmpTrack.Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> SaveChangesAsync();
    }
}
