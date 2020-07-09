using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class LinqContainsHelper
    {
        /// <summary>
        /// linq用于EF的contains查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="ids"></param>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<T> ContainsHelper<T, V>( List<V> ids,Func<List<V>, IQueryable<T>> predicate, int pageSize = 1000) where T : class
        {
            List<T> list = new List<T>();
            for (int i = 0; i < ids.Count; i = i + pageSize)
            {
                var tempIds = ids.OrderBy(o => o).Skip(i).Take(pageSize).ToList();
                var tempList = predicate(tempIds).ToList();
                list.AddRange(tempList);
            }
            return list;
        }
    }
}
