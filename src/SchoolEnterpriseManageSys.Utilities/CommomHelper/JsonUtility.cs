using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Converters;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public static class JsonUtility
    {
        public static string ToJson(object obj, bool indent, bool bareTableConvertor = false)
        {
            return ToJson(obj, indent, "yyyy-MM-dd HH:mm:ss", bareTableConvertor);
        }

        public static string ToJson(object obj, bool indent, string dateTimeFormat, bool bareTableConvertor = false)
        {
            return ToJson(obj, indent, dateTimeFormat, bareTableConvertor, false);
        }

        public static string ToJson(object obj,bool includeNullValue=true)
        {
            return ToJson(obj, false, "yyyy-MM-dd HH:mm:ss", false, includeNullValue);
        }

        public static string ToJson(object obj, bool indent, string dateTimeFormat, bool bareTableConvertor, bool includeNullValue)
        {
            var type = obj.GetType();

            var json = new JsonSerializer
            {
                NullValueHandling = includeNullValue? NullValueHandling.Include: NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            if (type == typeof(DataRow))
                json.Converters.Add(new DataRowConverter());
            else if (type == typeof(DataTable))
                json.Converters.Add(new DataTableConverter { OnlyRowArray = bareTableConvertor });
            else if (type == typeof(DataSet))
                json.Converters.Add(new DataSetConverter());

            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = dateTimeFormat;
            json.Converters.Add(iso);

            using (var sw = new StringWriter())
            {
                using (var writer = new JsonTextWriter(sw)
                {
                    Formatting = indent ? Formatting.Indented : Formatting.None,
                    QuoteChar = '"'
                })
                {
                    json.Serialize(writer, obj);

                    var jsonResult = sw.ToString();
                    writer.Close();
                    sw.Close();

                    return jsonResult;
                }
            }
        }

        public static string ObjectToJson(this object source, bool indent = false, bool bareTableConvertor = false)
        {
            if (source == null) return string.Empty;
            return ToJson(source, indent, bareTableConvertor);
        }

        public static object FromJson(string jsonstr, Type type)
        {
            return JsonConvert.DeserializeObject(jsonstr, type);
        }

        public static T FromJsonTo<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return default(T);
            var json = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            using (var sr = new StringReader(jsonString))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    var result = json.Deserialize<T>(reader);
                    return result;
                }
            }
        }

        private static string ConvertJsonDateToDateString(Match m)
        {
            var dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            var result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }
    }

    /// <summary>
    /// 定义DataRowConverter,继承JsonConverter,重写WriteJson的方法
    /// 使用反射机制来读DataRow的键值
    /// </summary>
    public class DataRowConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object dataRow, JsonSerializer serializer)
        {
            var row = dataRow as DataRow;
            writer.WriteStartObject();
            foreach (DataColumn column in row.Table.Columns)
            {
                writer.WritePropertyName(column.ColumnName);
                serializer.Serialize(writer, row[column]);
            }
            writer.WriteEndObject();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DataRow).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 定义DataTableConverter,继承JsonConverter,重写WriteJson的方法
    /// 使用反射机制来读DataTable的键值
    /// </summary>
    public class DataTableConverter : JsonConverter
    {
        public bool OnlyRowArray { get; set; }

        public override void WriteJson(JsonWriter writer, object dataTable, JsonSerializer serializer)
        {
            DataTable table = dataTable as DataTable;
            DataRowConverter converter = new DataRowConverter();

            if (!this.OnlyRowArray)
            {
                writer.WriteStartObject();
                writer.WritePropertyName(table.TableName);
            }
            writer.WriteStartArray();
            foreach (DataRow row in table.Rows)
            {
                converter.WriteJson(writer, row, serializer);
            }
            writer.WriteEndArray();
            if (!this.OnlyRowArray)
                writer.WriteEndObject();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DataTable).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 定义DataSetConverter,继承JsonConverter,重写WriteJson的方法
    /// 使用反射机制来读DataSet的键值
    /// </summary>
    public class DataSetConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object dataSet, JsonSerializer serializer)
        {
            DataSet ds = dataSet as DataSet;

            DataTableConverter converter = new DataTableConverter();
            writer.WriteStartObject();
            writer.WritePropertyName("Tables");
            writer.WriteStartArray();

            foreach (DataTable table in ds.Tables)
            {
                converter.WriteJson(writer, table, serializer);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DataSet).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
