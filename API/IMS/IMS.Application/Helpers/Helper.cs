using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IMS.Domain.Enums.SortingParameters;

namespace IMS.Application.Helpers
{
    public static class Helper
    {
        public static IOrderedQueryable<T> Sort<T>(IQueryable<T> source, string columnName, SortOrder sortOrder)
        {
            var propertyInfo = typeof(T).GetProperty(columnName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"No property '{columnName}' found on type '{typeof(T).Name}'.", nameof(columnName));
            }

            var parameter = Expression.Parameter(typeof(T), "x");

            var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);

            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var methodName = sortOrder == SortOrder.ASC ? "OrderBy" : "OrderByDescending";

            var result = Expression.Call(
                typeof(Queryable),
                methodName,
                [typeof(T), propertyInfo.PropertyType],
                source.Expression,
                orderByExp);

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(result);
        }
    }
}
