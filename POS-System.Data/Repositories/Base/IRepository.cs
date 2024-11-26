using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace POS_System.Data.Repositories.Base;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<T?> GetByExpressionWithIncludesAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);

    Task<(List<T> Results, int TotalCount)> GetByExpressionWithIncludesAndPaginationAsync(
        Expression<Func<T, bool>> predicate, int pageSize, int pageNumber,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);

    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<T> Results, int TotalCount)> GetAllWithPaginationAsync(
    int pageSize, int pageNumber, CancellationToken cancellationToken = default);

    Task<List<T>> GetAllByExpressionWithIncludesAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);

    Task<(List<T> Results, int TotalCount)> GetAllWithIncludesAndPaginationAsync(int pageSize, int pageNumber,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);

    Task<List<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task CreateAsync(T entity, CancellationToken cancellationToken = default);

    void Delete(T entity);
}
