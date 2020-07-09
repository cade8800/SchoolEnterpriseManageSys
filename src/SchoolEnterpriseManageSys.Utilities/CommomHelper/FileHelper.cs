using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class FileHelper
    {
        /// <summary>
        /// 读取文件，返回文本内容
        /// </summary>
        /// <param name="path">ReadHtmlText(HttpContext.Current.Server.MapPath("/Template/leftText.html"))</param>
        /// <returns></returns>
        public static string ReadHtmlText(string path)
        {
            Encoding code = Encoding.UTF8;
            StreamReader sr = null;
            string str = string.Empty;
            try
            {
                sr = new StreamReader(path, code);
                str = sr.ReadToEnd();   //   读取文件 
                sr.Close();
            }
            catch (Exception exp)
            {
                HttpContext.Current.Response.Write(exp.Message);
                HttpContext.Current.Response.End();
                sr.Close();
                return null;
            }
            return str;
        }

        public static string CreateHtml(string htmlfilename, string contentText)
        {

            Encoding code = Encoding.UTF8;
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(htmlfilename, false, code);
                sw.Write(contentText);
                sw.Flush();
                return "true";
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
                HttpContext.Current.Response.End();
                return "false" + ex.Message.ToString();
            }
            finally
            {
                sw.Close();
            }
        }
        public static string GetAllTxt(string relationPath = "")
        {
            try
            {
                string ret = "";
                if (relationPath == "") relationPath = string.Format("/Upload/mylog{0:yyyyMMdd}.txt", DateTime.Now);
                string txtPath = "";//获取绝对路径
                txtPath = GetAbsolutePath(relationPath);

                using (StreamReader sr = new StreamReader(txtPath, Encoding.Default))
                {
                    ret=sr.ReadToEnd();
                    sr.Close();
                }
                return ret;
            }
            catch { throw new Exception("Upload/文件夹没有读取权限！"); }
        }
        public static void AppendTxt(string text, string relationPath = "")
        {
            try
            {
                if (relationPath == "") relationPath = string.Format("/Upload/mylog{0:yyyyMMdd}.txt", DateTime.Now);
                string txtPath = "";//获取绝对路径
                txtPath = GetAbsolutePath(relationPath);

                using (StreamWriter sw = new StreamWriter(txtPath, true, Encoding.Default))
                {
                    sw.Write(text);
                    sw.Close();
                }
                return;
            }
            catch { throw new Exception("文件夹没有写入权限！"); }
        }

        /// <summary>
        /// 通过相对路径获取文件夹绝对路径
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static string GetAbsolutePath(string relativePath)
        {
            string AbsolutePath = "";
            if (HttpContext.Current != null)
            {
                AbsolutePath = HttpContext.Current.Server.MapPath(relativePath);
            }
            else
            {
                //多线程执行这里
                relativePath = relativePath.Replace("/", "\\");
                if (relativePath.StartsWith("\\"))//确定 String 实例的开头是否与指定的字符串匹配。为下边的合并字符串做准备
                {
                    relativePath = relativePath.TrimStart('\\');//从此实例的开始位置移除数组中指定的一组字符的所有匹配项。为下边的合并字符串做准备
                }
                AbsolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            }
            return AbsolutePath;
        }
    }
}
