namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Reflection;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;
    using SchoolEnterpriseManageSys.Core.Attributes;
    public static class EntityExtension
    {
        public static HashSet<Type> PrimitiveTypes = null;

        static EntityExtension()
        {
            PrimitiveTypes = new HashSet<Type>() 
                { 
                    typeof(String),
                    typeof(Byte[]),
                    typeof(Byte),
                    typeof(Int16),
                    typeof(Int32),
                    typeof(Int64),
                    typeof(Single),
                    typeof(Double),
                    typeof(Decimal),
                    typeof(DateTime),
                    typeof(Guid),
                    typeof(Boolean),
                    typeof(TimeSpan),
                    typeof(Byte?),
                    typeof(Int16?),
                    typeof(Int32?),
                    typeof(Int64?),
                    typeof(Single?),
                    typeof(Double?),
                    typeof(Decimal?),
                    typeof(DateTime?),
                    typeof(Guid?),
                    typeof(Boolean?),
                    typeof(TimeSpan?)
                };
        }


        static string[] ModifyFiled = { "ModifiedDate", "ModifiedByName", "ModifiedById", "ModifyUserId", "ModifyTime", "ModifyUserName" };
        public static string GetChangedFields<T>(this T newEntity, T oldEntity) where T : class
        {
            StringBuilder updatedFields = new StringBuilder();
            Type entityType = typeof(T);
            PropertyInfo[] properties = entityType.GetProperties().Where(o => o.CanWrite && PrimitiveTypes.Contains(o.PropertyType) && !o.GetCustomAttributes(false).OfType<NotMappedAttribute>().Any()).ToArray();
            foreach (var p in properties)
            {
                //去除修改时间等信息
                if (ModifyFiled.Contains(p.Name)) continue;

                object oldValue = p.GetValue(oldEntity, null);
                object newValue = p.GetValue(newEntity, null);
                if (oldValue == newValue)
                {
                    continue;
                }
                else if (oldValue == null && newValue != null || oldValue != null && newValue == null || !Eq(p.PropertyType, oldValue, newValue))
                {
                    string fieldName;
                    var display = p.GetCustomAttribute<LogAttribute>(false);
                    fieldName = display != null ? display.Name : p.Name;
                    if (display != null && display.EnumType != null)
                    {
                        oldValue = display.EnumToString(oldValue);
                        newValue = display.EnumToString(newValue);
                    }
                    updatedFields.AppendFormat("{0}:{1}->{2}; ", fieldName, oldValue ?? "NULL", newValue ?? "NULL");
                }
            }

            return updatedFields.ToString();
        }
        private static bool Eq(Type propertyType, object oldValue, object newValue)
        {
            if (propertyType == typeof(Decimal) || propertyType == typeof(Decimal?))
            {
                return decimal.Parse(oldValue.ToString()) == decimal.Parse(newValue.ToString());
            }
            else
            {
                return string.Equals(oldValue.ToString(), newValue.ToString());
            }
        }
    }
}