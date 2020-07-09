using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class DataMapper
    {
        public static void CopyValue<T>(T target, IDictionary source, params string[] propAccessors)
        {
            foreach (var propAccessor in propAccessors)
            {
                target.GetType().GetProperty(propAccessor).SetValue(target, source[propAccessor], null);
            }
        }
        public static void CopyValue<T>(T target, IDictionary source, params Expression<Func<T, object>>[] exps)
        {
            var list = new List<string>();
            foreach (var exp in exps)
            {
                list.Add(ExpressionHelper.GetPropertyName<T>(exp));
            }
            CopyValue<T>(target, source, list.ToArray());
        }
        public static void CopyValue<T>(T target, object source, params Expression<Func<T, object>>[] exps)
        {
            var list = new List<string>();
            foreach (var exp in exps)
            {
                list.Add(ExpressionHelper.GetPropertyName<T>(exp));
            }
            CopyValue<T>(target, source, list.ToArray());
        }
        public static void CopyValue<T>(T target, object source, params string[] propAccessors)
        {
            foreach (var propAccessor in propAccessors)
            {
                var value = source.GetType()
                    .GetProperty(propAccessor, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .GetValue(source);
                target.GetType().GetProperty(propAccessor).SetValue(target, value, null);
            }
        }
        public static void CopyValueNoError<T>(T target, IDictionary source, params Expression<Func<T, object>>[] exps)
        {
            var list = new List<string>();
            foreach (var exp in exps)
            {
                list.Add(ExpressionHelper.GetPropertyName<T>(exp));
            }
            CopyValueNoError<T>(target, source, list.ToArray());
        }
        public static void CopyValueNoError<T>(T target, object source, params Expression<Func<T, object>>[] exps)
        {
            var list = new List<string>();
            foreach (var exp in exps)
            {
                list.Add(ExpressionHelper.GetPropertyName<T>(exp));
            }
            CopyValueNoError<T>(target, source, list.ToArray());
        }
        public static void CopyValueNoError<T>(T target, object source, params string[] propAccessors)
        {
            foreach (var propAccessor in propAccessors)
            {
                try
                {
                    var value = source.GetType()
                   .GetProperty(propAccessor, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                   .GetValue(source);
                    target.GetType().GetProperty(propAccessor).SetValue(target, value, null);
                }
                catch (Exception)
                {
                }
            }
        }
        public static void CopyValueNoError<T>(T target, IDictionary source, params string[] propAccessors)
        {
            foreach (var propAccessor in propAccessors)
            {
                try
                {
                    target.GetType().GetProperty(propAccessor).SetValue(target, source[propAccessor], null);
                }
                catch (Exception)
                {
                }
            }
        }
    }


}
