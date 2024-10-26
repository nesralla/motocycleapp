using System;
using System.Linq;
using System.Linq.Expressions;

namespace Motocycle.Infra.CrossCutting.Commons.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condiction, Expression<Func<T, bool>> whereClause)
        {
            if (condiction)
                return query.Where(whereClause);

            return query;
        }
    }
}
