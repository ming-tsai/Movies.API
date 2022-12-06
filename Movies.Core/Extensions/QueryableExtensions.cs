using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Requests;
using Movies.Core.Responses;
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
}