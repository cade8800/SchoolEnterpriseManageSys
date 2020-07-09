using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class ArrayHelper
    {
        /// <summary>
        /// 将一维数组拆分为二维（携程接口要求）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<List<T>> GetList<T>(List<T> list, int count)
        {
            var ret = new List<List<T>>();

            //每次只推送30个Item
            //var r = list.Count % count == 0 ? list.Count / count : (list.Count / count + 1);

            //if (list.Count % count == 0)
            //{
            //    //整除部分
            //    for (int i = 0; i < list.Count / count; i++)
            //    {
            //        var temp = new List<T>();
            //        for (int j = 0; j < count; j++)
            //        {

            //        }
            //    }
            //}

            var temp = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                temp.Add(list[i]);
                if (temp.Count == count || i == list.Count - 1)
                {
                    ret.Add(temp);
                    temp = new List<T>();
                }
            }

            return ret;
        }
    }
}
