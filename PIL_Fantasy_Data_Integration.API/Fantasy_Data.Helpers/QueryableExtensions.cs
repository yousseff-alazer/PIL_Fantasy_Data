using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> FilterDynamic<T>(this IQueryable<T> query, string fieldName, ICollection<string> values)
        {
            var param = Expression.Parameter(typeof(T), "e");
            var prop = Expression.PropertyOrField(param, fieldName);
            var body = Expression.Call(typeof(Enumerable), "Contains", new[] { typeof(string) },
                Expression.Constant(values), prop);
            var predicate = Expression.Lambda<Func<T, bool>>(body, param);
            return query.Where(predicate);
        }
    }
}
