using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class XMLUtility
    {
        #region XML 序列化/反序列化
        /// <summary>
        /// XML序列化
        /// </summary>
        /// <typeparam name="T">需要序列化的变量的类型</typeparam>
        /// <param name="data">需要序列化的变量</param>
        /// <returns>序列化后的字符串</returns>
        public static string XmlSerializer<T>(T data)
        {
            //20150402--yxy
            return XmlSerializer<T>(data, false, Encoding.UTF8);
        }

        /// <summary>
        /// XML序列化移除开头问号
        /// </summary>
        /// <typeparam name="T">需要序列化的变量的类型</typeparam>
        /// <param name="data">需要序列化的变量</param>
        /// <returns>序列化后的字符串</returns>
        public static string XmlSerializerRemoveMark<T>(T data, bool omitXmlDeclaration = false,Dictionary<string, string> namespaces = null)
        {
            return XmlSerializer<T>(data, omitXmlDeclaration, new UTF8Encoding(false), namespaces);
        }



        /// <summary>
        /// XML序列化
        /// <remarks></remarks>
        /// </summary>
        /// <typeparam name="T">需要序列化的变量的类型</typeparam>
        /// <param name="data">需要序列化的变量</param>
        /// <param name="omitXmlDeclaration">是否忽略 XML 声明的值。true:省略</param>
        /// <param name="encoding">需要使用的字符串编码</param>
        /// <returns>序列化后的字符串</returns>
        public static string XmlSerializer<T>(T data, bool omitXmlDeclaration, Encoding encoding, Dictionary<string, string> namespaces = null)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                if (namespaces == null)
                {
                    ns.Add("", "");
                }
                else
                {
                    foreach (var key in namespaces.Keys)
                    {
                        ns.Add(key, namespaces[key]);
                    }
                }

                using (var xmlWriter =
                    XmlTextWriter.Create(stream,
                        new XmlWriterSettings()
                        {
                            OmitXmlDeclaration = omitXmlDeclaration,
                            Encoding = encoding,
                            Indent = false,
                            NewLineHandling = NewLineHandling.None
                        }))
                {
                    var s = new XmlSerializer(typeof(T));
                    s.Serialize(xmlWriter, data, ns);
                    s = null;
                }
                return encoding.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// XML序列化
        /// <remarks></remarks>
        /// </summary>
        /// <typeparam name="T">需要序列化的变量的类型</typeparam>
        /// <param name="data">需要序列化的变量</param>
        /// <param name="omitXmlDeclaration">是否忽略 XML 声明的值。true:省略</param>
        /// <param name="encoding">需要使用的字符串编码</param>
        /// <param name="isNeedNamespaxes">是否生成xml命名空间</param>
        /// <returns>序列化后的字符串</returns>
        public static string XmlSerializer<T>(T data, bool omitXmlDeclaration, Encoding encoding, bool isNeedNamespaxes)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializerNamespaces ns = null;
                if (!isNeedNamespaxes)
                {
                    ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                }
                using (var xmlWriter =
                    XmlTextWriter.Create(stream,
                        new XmlWriterSettings()
                        {
                            OmitXmlDeclaration = omitXmlDeclaration,

                            Encoding = encoding,
                            Indent = false,
                            NewLineHandling = NewLineHandling.None
                        }))
                {
                    var s = new XmlSerializer(typeof(T));
                    s.Serialize(xmlWriter, data, ns);
                    s = null;
                }
                return encoding.GetString(stream.ToArray());
            }
        }
        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T">反序列化结果的类型</typeparam>
        /// <param name="data">需要反序列化的字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T XmlDeSerializer<T>(string data)
        {
            T result = default(T);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(data);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            result = (T)serializer.Deserialize(new XmlNodeReader(xmlDoc.DocumentElement));
            serializer = null;
            xmlDoc = null;

            return result;
        }
        /// <summary>
        /// 字符串转换为对象
        /// </summary>
        /// <typeparam name="T"><peparam>
        /// <param name="t"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static TResult XmlToObject<TResult>(string xml) where TResult : class
        {
            XmlSerializer formatter = new XmlSerializer(typeof(TResult));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                TResult result = formatter.Deserialize(ms) as TResult;
                return result;
            }
        }
        #endregion

        public static string ObjectToXml<TData>(TData obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(TData));
                //序列化对象  
                xml.Serialize(stream, obj);
                stream.Position = 0;
                using (StreamReader sr = new StreamReader(stream))
                {
                    string str = sr.ReadToEnd();
                    return str;
                }
            }
        }

        #region 把xml格式字符串转换成XML格式

        /// <summary>
        /// 把xml格式字符串转换成XML格式
        /// </summary>
        /// <param name="strXML">xml格式字符串</param>
        /// <returns>转换失败返回null</returns>
        public static XmlDocument ConvertToXML(string strXML)
        {
            XmlDocument xmlDoc = null;
            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strXML);
            }
            catch
            {
                xmlDoc = null;
            }
            return xmlDoc;
        }
        #endregion

    }
}
