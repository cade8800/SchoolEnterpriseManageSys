using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    /// <summary>
    /// 构造函数使用True时：单个AND有效，多个AND有效；单个OR无效，多个OR无效；混合时写在AND后的OR有效
    /// 构造函数使用False时：单个AND无效，多个AND无效；单个OR有效，多个OR有效；混合时写在OR后面的AND有效
    /// </summary>
    public static class PredicateExtensions
    {
        //public static Expression<Func<T, bool>> True<T>() { return f => true; }

        //public static Expression<Func<T, bool>> False<T>() { return f => false; }

        //public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        //{
        //    var invokedExpression = Expression.Invoke(expression2, expression1.Parameters);

        //    return Expression.Lambda<Func<T, bool>>(Expression.Or(expression1.Body, invokedExpression), expression1.Parameters);
        //}

        //public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        //{
        //    var invokedExpression = Expression.Invoke(expression2, expression1.Parameters);

        //    return Expression.Lambda<Func<T, bool>>(Expression.And(expression1.Body, invokedExpression), expression1.Parameters);
        //}


        static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> source, string property, bool isAscdening = false)
        {
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            var pi = type.GetProperty(property);
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
        /// Linq动态排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">要排序的数据源</param>
        /// <param name="isAscdening"></param>
        /// <returns>IOrderedQueryable</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, bool isAscdening = false)
        {
            var orderSortName = isAscdening ? "OrderBy" : "ThenByDescending";
            return ApplyOrder(source, orderSortName);
        }

        /// <summary>
        /// Linq动态排序再排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">要排序的数据源</param>
        /// <param name="sortName"></param>
        /// <param name="isAscdening"></param>
        /// <returns>IOrderedQueryable</returns>
        //public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string sortName, bool isAscdening = false)
        //{
        //   // var = isAscdening ? "ThenBy" : "ThenByDescending";
        //    //return ApplyOrder<T>(source, orderSortName);
        //}


        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            var type = typeof(T);
            var arg = Expression.Parameter(type, "a");
            var pi = type.GetProperty(property);
            Expression expr = Expression.Property(arg, pi);
            type = pi.PropertyType;
            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);
            object result = typeof(Queryable).GetMethods().Single(
                a => a.Name == methodName
                     && a.IsGenericMethodDefinition
                     && a.GetGenericArguments().Length == 2
                     && a.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

    }
}