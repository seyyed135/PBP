using Microsoft.EntityFrameworkCore;
using PBP.DataAccess.Context;
using System.Linq.Expressions;

namespace PBP.DataAccess.Repository;

public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();


    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetObjectAsync(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(filter);
        return await query.SingleOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> filter) => await _dbSet.Where(filter).ToListAsync();

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter) => await _dbSet.AnyAsync(filter);

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public void DeleteRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}