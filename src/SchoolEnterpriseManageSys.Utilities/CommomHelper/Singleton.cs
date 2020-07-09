using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class Singleton<T> : Singleton
    {
        static T instance;

        public static T Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
                All[typeof(T)] = value;
            }
        }
    }

    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        public new static IList<T> Instance {
            get
            {
                return Singleton<IList<T>>.Instance;
            }
        }
    }

    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonDictionary()
        {
            Singleton<IDictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        public new static IDictionary<TKey, TValue> Instance {
            get
            {
                return Singleton<IDictionary<TKey, TValue>>.Instance;
            }
        }
    }

    public class Singleton
    {
        static Singleton()
        {
            all = new Dictionary<Type, object>();
        }

        static readonly IDictionary<Type, object> all;

        public static IDictionary<Type, object> All
        {
            get
            {
                return all;
            }
        }
    }
}
