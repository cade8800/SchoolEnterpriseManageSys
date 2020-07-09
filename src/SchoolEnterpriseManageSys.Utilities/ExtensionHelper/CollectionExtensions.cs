


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using SchoolEnterpriseManageSys.Utilities.CommomHelper;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    /// <summary>
    ///     集合扩展方法类
    /// </summary>
    public static class CollectionsExtensions
    {
        #region 公共方法

        #endregion

        #region 辅助操作类

        private static class QueryableHelper<T>
        {
            // ReSharper disable StaticFieldInGenericType
            private static readonly ConcurrentDictionary<string, LambdaExpression> Cache = new ConcurrentDictionary<string, LambdaExpression>();

            internal static IOrderedQueryable<T> OrderBy(IQueryable<T> source, string propertyName, ListSortDirection sortDirection)
            {
                dynamic keySelector = GetLambdaExpression(propertyName);
                return sortDirection == ListSortDirection.Ascending
                    ? Queryable.OrderBy(source, keySelector)
                    : Queryable.OrderByDescending(source, keySelector);
            }

            internal static IOrderedQueryable<T> ThenBy(IOrderedQueryable<T> source, string propertyName, ListSortDirection sortDirection)
            {
                dynamic keySelector = GetLambdaExpression(propertyName);
                return sortDirection == ListSortDirection.Ascending
                    ? Queryable.ThenBy(source, keySelector)
                    : Queryable.ThenByDescending(source, keySelector);
            }

            private static LambdaExpression GetLambdaExpression(string propertyName)
            {
                if (Cache.ContainsKey(propertyName))
                {
                    return Cache[propertyName];
                }
                ParameterExpression param = Expression.Parameter(typeof(T));
                MemberExpression body = Expression.Property(param, propertyName);
                LambdaExpression keySelector = Expression.Lambda(body, param);
                Cache[propertyName] = keySelector;
                return keySelector;
            }
        }

        #endregion
    }
}