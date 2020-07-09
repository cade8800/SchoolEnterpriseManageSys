using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
   public static  class EnumerableExtensions
    {
       /// <summary>
       /// 确保集合非空
       /// </summary>
       /// <param name="source"></param>
       /// <returns></returns>
       public static IEnumerable<T> OpenSafe<T>(this IEnumerable<T> source)
       {
           if (source == null)
           {
               return Enumerable.Empty<T>();
           }

           return source;
       }
    }
}
