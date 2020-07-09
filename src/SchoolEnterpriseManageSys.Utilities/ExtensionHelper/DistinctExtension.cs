using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    public class PredicateEqualityComparer<T> : EqualityComparer<T>
    {
        private Func<T, T, bool> predicate;

        public PredicateEqualityComparer(Func<T, T, bool> predicate)
            : base()
        {
            this.predicate = predicate;
        }

        public override bool Equals(T x, T y)
        {
            if (x != null)
            {
                return ((y != null) && this.predicate(x, y));
            }

            if (y != null)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode(T obj)
        {
            // Always return the same value to force the call to IEqualityComparer<T>.Equals
            return 0;
        }
    }


    class CommonEqualityComparer<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;
        private IEqualityComparer<V> comparer;

        public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public CommonEqualityComparer(Func<T, V> keySelector)
            : this(keySelector, EqualityComparer<V>.Default)
        { }

        public bool Equals(T x, T y)
        {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(keySelector(obj));
        }
    }

    public static class DistinctExtension
    {
        /*
         * var ps3 = data3
         .Distinct(p => p.Name, StringComparer.CurrentCultureIgnoreCase)
         .ToArray();*/
        public static IEnumerable<T> DistinctEx<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }

        public static IEnumerable<T> DistinctEx<T, V>(this IEnumerable<T> source, Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector, comparer));
        }


        //扩展方法 XXX.Distinct((x, y) => x.Field == y.Field)
        public static IEnumerable<TSource> DistinctEx<TSource>
            (this IEnumerable<TSource> source, Func<TSource, TSource, bool> predicate)
        {
            return source.Distinct(new PredicateEqualityComparer<TSource>(predicate));
        }
    }
}
