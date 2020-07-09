using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]//设置了定位参数和命名参数
    public class LogAttribute : Attribute
    {
        public LogAttribute()
        {

        }

        public LogAttribute(string name, Type enumType = null)
        {
            this.Name = name;
            this.EnumType = EnumType;
        }

        public string Name { get; set; }

        public Type EnumType { get; set; }

        public string EnumToString(object input)
        {
            if (input == null)
            {
                return "";
            }
            FieldInfo info = EnumType.GetField(Enum.GetName(EnumType, input));
            DescriptionAttribute descriptionAttribute = info.GetCustomAttributes(typeof(DescriptionAttribute), true)[0] as DescriptionAttribute;
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;
            else
                return EnumType.ToString();
        }
    }
}
