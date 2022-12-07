using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Utils;
using Movies.Domain.Entities;

namespace Movies.Core.Interfaces;
public static class QueryableExtensions
{
    public static IQueryable<TSource> Where<TSource>(
        this IQueryable<TSource> query, string propertyName, string? value)
    {
        Check.NotNull(query, nameof(query));
        Check.NotEmpty(propertyName, nameof(propertyName));
        if (!string.IsNullOrEmpty(value))
        {
            var pattern = Expression.Constant($"%{value}%");
            var parameter = Expression.Parameter(typeof(Movie));
            var expression = Expression.Call(
                typeof(DbFunctionsExtensions), "Like", Type.EmptyTypes,
                Expression.Constant(EF.Functions),
                Expression.Property(parameter, propertyName), pattern);
            var lambda = Expression.Lambda<Func<TSource, bool>>(expression, parameter);
            query = query.Where(lambda);
        }
        return query;
    }

    public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string? sorting)
    {
        Check.NotNull(source, nameof(source));
        if (!string.IsNullOrEmpty(sorting))
        {
            source = GetSortQuery(source, sorting);
        }
        return source;
    }

    private static IQueryable<TSource> GetSortQuery<TSource>(IQueryable<TSource> source, string sorting)
    {
        var sourceType = typeof(TSource);
        var param = Expression.Parameter(typeof(TSource));
        var isDescending = sorting.EndsWith("desc", StringComparison.CurrentCultureIgnoreCase) ||
                    sorting.EndsWith("descending", StringComparison.CurrentCultureIgnoreCase);
        var propertyName = string.Empty;
        if (sorting.Contains("_", StringComparison.OrdinalIgnoreCase))
        {
            propertyName = sorting.Substring(0, sorting.IndexOf("_", StringComparison.OrdinalIgnoreCase));
        }
        else if (sorting.Contains(" ", StringComparison.OrdinalIgnoreCase))
        {
            propertyName = sorting.Substring(0, sorting.IndexOf(" ", StringComparison.OrdinalIgnoreCase));
        }
        PropertyInfo? property = null;
        if (!string.IsNullOrEmpty(propertyName))
        {
            property = typeof(TSource).GetProperties()
                .FirstOrDefault(e => e.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
        }

        if (property != null)
        {
            var sortExpression = Expression.Lambda<Func<TSource, object>>
                    (Expression.Convert(Expression.Property(param, property.Name), typeof(object)), param);
            source = isDescending ? source.OrderByDescending(sortExpression) : source.OrderBy(sortExpression);
        }

        return source;
    }
}