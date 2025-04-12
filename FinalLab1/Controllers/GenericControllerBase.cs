using System.Linq.Expressions;
using FinalLab1.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalLab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class GenericControllerBase<T, TKey> : ControllerBase
        where T : class
    {
       private readonly IGenericService<T,TKey> _service;

        protected GenericControllerBase(IGenericService<T,TKey> service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] TKey ? id)
        {
            
            var result = await _service.GetByIdAsync(id); 
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] T entity)
        {
            var created = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = GetEntityId(created) }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] TKey  id, [FromBody] T entity)
        {
            var updated = await _service.UpdateAsync(entity);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] TKey  id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? Ok("Delete Success") : NotFound();
        }

        [HttpGet("find")]
        public async Task<IActionResult> FindByCondition([FromQuery] string[] field, [FromQuery] string[] value)
        {
            if (field.Length != value.Length)
                return BadRequest("Số lượng field và value không khớp.");

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? finalExpression = null;

            for (int i = 0; i < field.Length; i++)
            {
                var property = Expression.Property(parameter, field[i]);
                var propertyType = property.Type;

                object? typedValue;

                // ✅ Xử lý Enum
                if (propertyType.IsEnum)
                {
                    typedValue = Enum.Parse(propertyType, value[i], ignoreCase: true);
                }
                else if (Nullable.GetUnderlyingType(propertyType)?.IsEnum == true)
                {
                    var enumType = Nullable.GetUnderlyingType(propertyType)!;
                    typedValue = Enum.Parse(enumType, value[i], ignoreCase: true);
                }
                else
                {
                    typedValue = Convert.ChangeType(value[i], Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                }

                var constant = Expression.Constant(typedValue, propertyType);
                var equal = Expression.Equal(property, constant);

                // Gộp các biểu thức lại bằng AND
                finalExpression = finalExpression == null
                    ? equal
                    : Expression.AndAlso(finalExpression, equal);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(finalExpression!, parameter);
            var result = await _service.FindByConditionAsync(lambda);
            return Ok(result);
        }


        [HttpGet("include")]
        public async Task<IActionResult> FindWithInclude([FromQuery] string[] includes)
        {
            var includeExpressions = includes
                .Select(i => CreateIncludeExpression(i))
                .Where(e => e != null)
                .Cast<Expression<Func<T, object>>>()
                .ToArray();

            var result = await _service.FindWithIncludeAsync(null, includeExpressions);
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _service.GetPagedAsync(page, size);
            return Ok(result);
        }
        [HttpGet("paged-condition")]
        public async Task<IActionResult> GetPagedcondition(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromQuery(Name = "filters")] List<string> filters,
            [FromQuery] string[] includes)
        {
            var includeExpressions = includes?
                .Select(i => CreateIncludeExpression(i))
                .Where(e => e != null)
                .Cast<Expression<Func<T, object>>>()
                .ToArray() ?? Array.Empty<Expression<Func<T, object>>>();

            var result = await _service.GetPagedWithIncludeWithCoditionAsync(pageIndex, pageSize, filters, includeExpressions);
            return Ok(result);
        }
        [HttpGet("paged-include")]
        public async Task<IActionResult> GetPagedWithInclude([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string[] includes = null!)
        {
            var includeExpressions = includes?
                .Select(i => CreateIncludeExpression(i))
                .Where(e => e != null)
                .Cast<Expression<Func<T, object>>>()
                .ToArray() ?? [];

            var result = await _service.GetPagedWithIncludeAsync(page, size, null, includeExpressions);
            return Ok(result);
        }

        /// <summary>
        /// Override ở subclass để trả về đúng ID dùng cho CreatedAtAction.
        /// </summary>
        protected virtual object GetEntityId(T entity) => 0;
        private Expression<Func<T, object>>? CreateIncludeExpression(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, propertyName);

            // ⚠️ Nếu là reference type (class, ICollection, v.v.) thì không cần convert
            if (!property.Type.IsValueType)
            {
                return Expression.Lambda<Func<T, object>>(property, parameter);
            }

            // ✅ Nếu là value type (int, DateTime, bool, ...) thì phải convert
            var converted = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<T, object>>(converted, parameter);
        }
      

    }
}