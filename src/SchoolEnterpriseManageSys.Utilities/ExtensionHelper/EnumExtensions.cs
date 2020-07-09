


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    /// <summary>
    ///     枚举扩展方法类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            Type type = value.GetType();
            MemberInfo member = type.GetMember(value.ToString()).FirstOrDefault();
            return member != null ? member.ToDescription() : value.ToString();
        }


        /// <summary>
        /// 根据枚举类型返回类型中的所有值，文本及描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns>返回三列数组，第0列为Description,第1列为Value，第2列为Text</returns>
        public static IEnumerable<EnumItem> GetEnumOpt(this Type type)
        {
            var names = Enum.GetNames(type);

            FieldInfo[] fields = type.GetFields();
            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                var desc = field.Name;
                if (!names.Contains(desc)) continue;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs != null && objs.Length > 0)
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    desc = da.Description;
                }

                yield return new EnumItem { Description = desc, Name = field.Name, Value = (int)Enum.Parse(type, field.Name) };
            }
        }

        public static List<int> GetEnumValues(this Type type)
        {
            List<int> list = new List<int>();
            var fis = type.GetFields();
            fis.ToList().ForEach((field) =>
            {
                if (!field.Name.Contains("value_"))
                {
                    list.Add((int)Enum.Parse(type, field.Name));
                }
            });

            return list;
        }
    }

    public class EnumItem
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}