using System.Linq.Expressions;
using FinalLab1.Converter;
using FinalLab1.Data;
using FinalLab1.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FinalLab1.Services;

public class GenericService<T, TKey> : IGenericService<T, TKey> where T : class
{
    private readonly LabDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericService(LabDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(TKey? id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(TKey id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindWithIncludeAsync(
        Expression<Func<T, bool>>? predicate = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        // Apply includes nếu có
        if (includes != null)
        {
            foreach (var include in includes)
            {
                var body = include.Body;

                // ⚠️ Bỏ Convert(...) nếu có
                if (body is UnaryExpression unary && unary.NodeType == ExpressionType.Convert)
                {
                    if (unary.Operand is MemberExpression member)
                    {
                        var cleanedInclude = Expression.Lambda<Func<T, object>>(member, include.Parameters);
                        query = query.Include(cleanedInclude);
                    }
                }
                else if (body is MemberExpression)
                {
                    query = query.Include(include);
                }
            }
        }

        // Apply điều kiện lọc nếu có
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }
    public async Task<PagedResult<T>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = _dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }
    public async Task<PagedResult<T>> GetPagedWithIncludeAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }
    public async Task<PagedResult<T>> GetPagedWithIncludeWithCoditionAsync(
        int pageIndex,
        int pageSize,
        List<string> filters,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        var predicate = PredicateBuilder<T>.BuildPredicateFromFilters(filters);
        Console.WriteLine(predicate.ToString());
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }
}