using System.Linq.Expressions;
using FinalLab1.Dtos;

namespace FinalLab1.Services;

public interface IGenericService<T, TKey> where T : class
{
    Task<T?> GetByIdAsync(TKey? id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(TKey id);
    Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> FindWithIncludeAsync(
        Expression<Func<T, bool>>? predicate = null,
        params Expression<Func<T, object>>[] includes);
    Task<PagedResult<T>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? predicate = null);
    Task<PagedResult<T>> GetPagedWithIncludeAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        params Expression<Func<T, object>>[] includes);
    Task<PagedResult<T>> GetPagedWithIncludeWithCoditionAsync(
        int pageIndex,
        int pageSize,
        List<string> filters,
        params Expression<Func<T, object>>[] includes);
}