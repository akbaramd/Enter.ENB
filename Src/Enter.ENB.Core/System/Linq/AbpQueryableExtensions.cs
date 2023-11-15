﻿using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Enter.ENB.Statics;

namespace System.Linq;

/// <summary>
/// Some useful extension methods for <see cref="IQueryable{T}"/>.
/// </summary>
public static class AbpQueryableExtensions
{
    /// <summary>
    /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
    /// </summary>
    public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
    {
        EntCheck.NotNull(query, nameof(query));

        return query.Skip(skipCount).Take(maxResultCount);
    }

    /// <summary>
    /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
    /// </summary>
    public static TQueryable PageBy<T, TQueryable>(this TQueryable query, int skipCount, int maxResultCount)
        where TQueryable : IQueryable<T>
    {
        EntCheck.NotNull(query, nameof(query));

        return (TQueryable)query.Skip(skipCount).Take(maxResultCount);
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        EntCheck.NotNull(query, nameof(query));

        return condition
            ? query.Where(predicate)
            : query;
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static TQueryable WhereIf<T, TQueryable>(this TQueryable query, bool condition, Expression<Func<T, bool>> predicate)
        where TQueryable : IQueryable<T>
    {
        EntCheck.NotNull(query, nameof(query));

        return condition
            ? (TQueryable)query.Where(predicate)
            : query;
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
    {
        EntCheck.NotNull(query, nameof(query));

        return condition
            ? query.Where(predicate)
            : query;
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static TQueryable WhereIf<T, TQueryable>(this TQueryable query, bool condition, Expression<Func<T, int, bool>> predicate)
        where TQueryable : IQueryable<T>
    {
        EntCheck.NotNull(query, nameof(query));

        return condition
            ? (TQueryable)query.Where(predicate)
            : query;
    }

    /// <summary>
    /// Order a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="sorting">Order the query</param>
    /// <returns>Order or not order query based on <paramref name="condition"/></returns>
    public static TQueryable OrderByIf<T, TQueryable>(this TQueryable query, bool condition, string sorting)
        where TQueryable : IQueryable<T>
    {
        EntCheck.NotNull(query, nameof(query));

        return condition
            ? (TQueryable)Dynamic.Core.DynamicQueryableExtensions.OrderBy(query, sorting)
            : query;
    }
}
