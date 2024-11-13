using Microsoft.EntityFrameworkCore;
using POS_System.Data.Database;
using POS_System.Data.Repositories.Base;
using System.Linq.Expressions;

namespace MedicalCenter.Data.Repositories.Base;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext<int> _dbContext;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext<int> dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public async Task<T?> GetByExpressionWithIncludesAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
    {
        return await ApplyIncludes(_dbSet.AsQueryable(), includes).FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<(List<T> Results, int TotalCount)> GetByExpressionWithIncludesAndPaginationAsync(Expression<Func<T, bool>> predicate,
       int pageSize, int pageNumber, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
    {
        var query = ApplyIncludes(_dbSet.AsQueryable(), includes).Where(predicate);
        var totalCount = await query.CountAsync(cancellationToken);
        var results = await query.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (results, totalCount);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<List<T>> GetAllByExpressionWithIncludesAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
    {
        return await ApplyIncludes(_dbSet.AsQueryable(), includes).Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<List<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<(List<T> Results, int TotalCount)> GetAllWithIncludesAndPaginationAsync(int pageSize, int pageNumber,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
    {
        var query = ApplyIncludes(_dbSet.AsQueryable(), includes);
        var totalCount = await query.CountAsync(cancellationToken);
        var results = await query.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (results, totalCount);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    private static IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
    {
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}