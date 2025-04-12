using System.Linq.Expressions;
using System.Reflection;

namespace FinalLab1.Converter;

public static class PredicateBuilder<T>
{
    public static Expression<Func<T, bool>>? BuildPredicateFromFilters(List<string> filters)
    {
        if (!filters.Any()) return null;

        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        Expression? finalBody = null;

        foreach (var filter in filters)
        {
            var parts = filter.Split(':', 3);
            if (parts.Length != 3) continue;

            var propertyName = parts[0];
            var operatorStr = parts[1].ToLower();
            var valueStr = parts[2];

            var propInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null) continue;

            var property = Expression.Property(parameter, propInfo);
            var targetType = Nullable.GetUnderlyingType(property.Type) ?? property.Type;

            object? value;
            try
            {
                value = targetType.IsEnum
                    ? (int.TryParse(valueStr, out var intVal)
                        ? Enum.ToObject(targetType, intVal)
                        : Enum.Parse(targetType, valueStr, true))
                    : Convert.ChangeType(valueStr, targetType);
            }
            catch
            {
                continue;
            }

            Expression? expression = operatorStr switch
            {
                "eq" => Expression.Equal(property, Expression.Constant(value)),
                "contains" => property.Type == typeof(string)
                    ? Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, Expression.Constant(value))
                    : null,
                _ => null
            };

            if (expression == null) continue;

            finalBody = finalBody == null
                ? expression
                : Expression.AndAlso(finalBody, expression);
        }

        return finalBody != null ? Expression.Lambda<Func<T, bool>>(finalBody, parameter) : null;
    }
}