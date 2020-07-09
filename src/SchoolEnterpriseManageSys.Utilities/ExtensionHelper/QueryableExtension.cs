using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    public static class QueryableExtension
    {
        public static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> source, string property, bool isAscdening = false)
        {
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            var pi = type.GetProperties().FirstOrDefault(c => c.Name.Equals(property, StringComparison.OrdinalIgnoreCase));
            if (pi == null)
            {
                throw new ExceptionHeper.BusinessException(string.Format("{0}类型不存在{1}属性，无法进行排序", type.Name, property));
            }
            expr = Expression.Property(expr, pi);
            type = pi.PropertyType;

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            object result;
            if (true == isAscdening)
            {
                result = typeof(Queryable).GetMethods().
                    Single(method => method.Name == "OrderBy" && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2).
                    MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            }
            else
            {
                result = typeof(Queryable).GetMethods().
                    Single(method => method.Name == "OrderByDescending" && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2).
                    MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            }
            return (IOrderedQueryable<T>)result;
        }

        /// <summary>
        /// 动态构建排序条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="field">例：LoginId 或 Login desc</param>
        /// <returns></returns>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string field)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            string sortDirection = String.Empty;
            string propertyName = String.Empty;

            field = field.Trim();
            int spaceIndex = field.Trim().IndexOf(" ");
            if (spaceIndex < 0)
            {
                propertyName = field;
                sortDirection = "asc";
            }
            else
            {
                propertyName = field.Substring(0, spaceIndex);
                sortDirection = field.Substring(spaceIndex + 1).Trim();
            }

            if (String.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, String.Empty);
            MemberExpression property = Expression.Property(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = (sortDirection == "desc") ? "OrderByDescending" : "OrderBy";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, property.Type },
                                                source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }



    }
}
