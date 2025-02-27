using System.Linq.Expressions;

namespace PBP.DataAccess.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetObjectAsync(Expression<Func<T, bool>> filter);

    Task<T?> GetByIdAsync(int id);

    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task SaveAsync();
}