

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    /// <summary>
    ///     字符串辅助操作类
    /// </summary>
    public class StringHelper
    {
        public static string str { get; set; }
        private const string randomChars = "ABCDE*()FG23456HIJKLM#$%NOPQRno&_pqST7SYZabcdefghiUWV1jklm+.~rstuwvsyz089!@^";
        /// <summary>
        ///     把对象转换成Json字符串表示形式
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string BuildJsonString(object jsonObject)
        {
            var ser = new JavaScriptSerializer();
            return ser.Serialize(jsonObject);
        }

        /// <summary>
        ///     判断指定字符串是否对象（Object）类型的Json字符串格式
        /// </summary>
        /// <param name="input">要判断的Json字符串</param>
        /// <returns></returns>
        public static bool IsJsonObjectString(string input)
        {
            return input != null && input.StartsWith("{") && input.EndsWith("}");
        }

        /// <summary>
        ///     判断指定字符串是否集合类型的Json字符串格式
        /// </summary>
        /// <param name="input">要判断的Json字符串</param>
        /// <returns></returns>
        public static bool IsJsonArrayString(string input)
        {
            return input != null && input.StartsWith("[") && input.EndsWith("]");
        }

        /// <summary>
        /// 生成验证码流
        /// </summary>
        /// <param name="verCode">生成的验证码</param>
        /// <param name="codeLen">验证码长度</param>
        /// <returns></returns>
        public static MemoryStream GetVerCode(out string verCode, int codeLen = 4)
        {
            verCode = string.Empty;
            int codeW = 80;
            int codeH = 22;
            int fontSize = 16;
            string chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            var rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < codeLen; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            verCode = chkCode;
            //创建画布
            var bmp = new Bitmap(codeW, codeH);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 1; i++)
            {
                int x1 = rnd.Next(codeW);
                int y1 = rnd.Next(codeH);
                int x2 = rnd.Next(codeW);
                int y2 = rnd.Next(codeH);
                var clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                var ft = new Font(fnt, fontSize);
                var clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18 + 2, (float)0);
            }
            //画噪点 
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }

            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            var ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();
            }
            return ms;
        }

        /// <summary>
        /// 检查密码是否符合强度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckPassord(string str)
        {
            var ch = str.ToCharArray();
            int numCount = 0;
            var charCount = 0;
            var markCount = 0;
            foreach (var a in ch)
            {
                if (a >= 33 && a <= 47) { markCount++; }//判断是特殊符
                if (a >= 48 && a <= 57) { numCount++; }//判断是数字
                if (a >= 58 && a <= 64) { markCount++; }//判断是特殊符
                if (a >= 65 && a <= 90) { charCount++; }//判断是大写字母
                if (a >= 91 && a <= 96) { markCount++; }//判断是特殊符
                if (a >= 97 && a <= 122) { charCount++; }//判断是小写字母
                if (a >= 123 && a <= 126) { markCount++; }//判断是特殊符
            }
            //最后NumCount就是数字的个数
            //最后CharCount就是数字的个数
            return numCount > 0 && charCount > 0 && markCount > 0;
        }
        /// <summary>
        /// 随机生成密码
        /// </summary>
        /// <param name="passwordLen">密码长度</param>
        /// <returns></returns>
        public static string GetRandomPassword(int passwordLen)
        {
            string password = string.Empty;
            int randomNum;
            Random random = new Random();
            for (int i = 0; i < passwordLen; i++)
            {
                randomNum = random.Next(randomChars.Length);
                password += randomChars[randomNum];
            }
            return password;
        }

        /// <summary>
        /// 获取将字符串中所有需要替换的标识
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="c">分割字符</param>
        /// <returns></returns>
        public static string[] GetSplitStrings(string str, char c)
        {
            var ss = str;
            var retList = new List<int>();
            var list = new List<string>();
            var i = -1;
            int nowIndex = -1;
            while (str.IndexOf(c) > -1)
            {
                int subIndex = str.IndexOf(c);
                nowIndex = subIndex + nowIndex + 1;
                retList.Add(nowIndex);
                str = str.Substring(subIndex + 1);
                i++;
            }
            for (var j = 0; j < retList.Count; )
            {
                if ((j + 1) >= retList.Count)
                    break;
                list.Add(ss.Substring(retList[j], (retList[j + 1] - retList[j]) + 1));
                j = j + 2;
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="beginSplit"></param>
        /// <param name="endSplit"></param>
        /// <returns></returns>
        public static List<string> GetForeachs(string source, char beginSplit = '{', char endSplit = '}')
        {
            var ss = source;
            var retList = new List<int>();
            var list = new List<string>();
            var i = -1;
            int nowIndex = -1;

            while (source.IndexOf(beginSplit) > -1 || source.IndexOf(endSplit) > -1)
            {
                int subIndex = source.IndexOf(beginSplit) != -1 && source.IndexOf(beginSplit) < source.IndexOf(endSplit) ? source.IndexOf(beginSplit) : source.IndexOf(endSplit);
                nowIndex = subIndex + nowIndex + 1;
                retList.Add(nowIndex);
                source = source.Substring(subIndex + 1);
                i++;
            }
            for (var j = 0; j < retList.Count; )
            {
                if ((j + 1) >= retList.Count)
                    break;
                list.Add(ss.Substring(retList[j], (retList[j + 1] - retList[j]) + 1));
                j = j + 2;
            }
            return list;
        }


        public static string FormatTemplateContent(string templateContent, string parasJson, char splitChar = '|', List<string> foreachKeys = null)
        {
            if (string.IsNullOrWhiteSpace(templateContent))
                return String.Empty;
            var jss = new JavaScriptSerializer();
            var parasDic0 = string.IsNullOrWhiteSpace(parasJson)
                 ? new Dictionary<string, object>()
                 : jss.Deserialize<Dictionary<string, object>>(parasJson);

            //有循环体的情况
            if (foreachKeys != null && foreachKeys.Count > 0)
            {
                //取出循环体
                var foreachList = GetForeachs(templateContent);
                //循环体的数据用list<string>转换
                if (foreachList != null && foreachList.Count > 0)
                {
                    //保存循环体的每次数据
                    Dictionary<int, Dictionary<string, string>> container = new Dictionary<int, Dictionary<string, string>>();
                    //取出循环体的数据保存到container，每次数据对应一个index
                    for (int i = 0; i < foreachKeys.Count; i++)
                    {
                        //取出每个循环的key对应的数据保存到index对应的value里面
                        var dataList = parasDic0[foreachKeys[i]] as System.Collections.ArrayList;
                        for (int j = 0; j < dataList.Count; j++)
                        {
                            if (container.ContainsKey(j))
                            {
                                container[j].Add(foreachKeys[i], dataList[j].ToString());
                            }
                            else
                            {
                                Dictionary<string, string> dataItem = new Dictionary<string, string>();
                                dataItem.Add(foreachKeys[i], dataList[j].ToString());
                                container.Add(j, dataItem);
                            }
                        }
                    }

                    //处理每一个循环体
                    for (int f = 0; f < foreachList.Count; f++)
                    {
                        //每个循环体都可以循环模板拼接
                        StringBuilder itemSb = new StringBuilder("");
                        //遍历container，每次生成一个模板对应的内容
                        foreach (var item1 in container)
                        {
                            string temp = foreachList[f];
                            var splitList0 = GetSplitStrings(temp, splitChar);
                            if (item1.Value != null && item1.Value.Count > 0 && splitList0.Length > 0)
                            {
                                int replaceCount = 0;
                                foreach (var s in splitList0)
                                {
                                    string parasValue0;
                                    if (item1.Value.TryGetValue(s.Replace(splitChar.ToString(CultureInfo.InvariantCulture), ""), out parasValue0))
                                    {
                                        temp = temp.Replace(s, parasValue0);
                                        replaceCount++;
                                    }
                                }
                                if (replaceCount > 0)
                                {
                                    temp = temp.Trim('{');
                                    temp = temp.Trim('}');
                                    itemSb.Append(temp + "，");
                                }
                            }
                        }
                        string itemContent = itemSb.ToString();
                        itemContent = f == (foreachList.Count - 1) && itemContent.LastIndexOf("，", StringComparison.InvariantCultureIgnoreCase) == itemContent.Length - 1 ? itemContent.Substring(0, itemContent.Length - 1) : itemContent;
                        templateContent = templateContent.Replace(foreachList[f], itemContent);
                    }
                }
                //处理循环体外的替换
                var splitList1 = GetSplitStrings(templateContent, splitChar);
                if (parasDic0 != null && (splitList1.Length > 0 && parasDic0.Count > 0))
                {
                    foreach (var s in splitList1)
                    {
                        object parasValue1;
                        if (parasDic0.TryGetValue(s.Replace(splitChar.ToString(CultureInfo.InvariantCulture), ""), out parasValue1))
                        {
                            templateContent = templateContent.Replace(s, parasValue1.ToString());
                        }
                    }
                }
            }
            else
            {
                var parasDic = string.IsNullOrWhiteSpace(parasJson)
                    ? new Dictionary<string, string>()
                    : jss.Deserialize<Dictionary<string, string>>(parasJson);
                var splitList = GetSplitStrings(templateContent, splitChar);
                if (parasDic != null && (splitList.Length > 0 && parasDic.Count > 0))
                {
                    foreach (var s in splitList)
                    {
                        //s.Replace("|","")主要是为了防止模板中有多个一样的标签
                        string parasValue;
                        parasDic.TryGetValue(s.Replace(splitChar.ToString(CultureInfo.InvariantCulture), ""), out parasValue);
                        templateContent = templateContent.Replace(s, parasValue);
                    }
                }
            }
            return templateContent.Replace(splitChar.ToString(CultureInfo.InvariantCulture), "");
        }

        /// <summary>
        /// 检查密码强度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool CheckPasswordStrong(string str)
        {

            var ch = str.ToCharArray();
            var numCount = 0;
            var charCount = 0;
            var markCount = 0;
            foreach (var a in ch)
            {
                if (a >= 33 && a <= 47) { markCount++; }//判断是特殊符
                if (a >= 48 && a <= 57) { numCount++; }//判断是数字
                if (a >= 58 && a <= 64) { markCount++; }//判断是特殊符
                if (a >= 65 && a <= 90) { charCount++; }//判断是大写字母
                if (a >= 91 && a <= 96) { markCount++; }//判断是特殊符
                if (a >= 97 && a <= 122) { charCount++; }//判断是小写字母
                if (a >= 123 && a <= 126) { markCount++; }//判断是特殊符
            }
            //最后NumCount就是数字的个数
            //最后CharCount就是数字的个数
            return numCount > 0 && charCount > 0 && markCount > 0;
        }

        public static string GenerateNumberCode(int strLength)
        {
            //  char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }; 
            //char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] constant = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(9);
            //DateTime now = DateTime.Now;
            // [color=#FF0000]//需要生成不同的随机数生成器
            //Random rd = new Random(now.Millisecond * now.Millisecond);// [/color]
            Random rd = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));

            for (int i = 0; i < strLength; i++)
            {
                newRandom.Append(constant[rd.Next(8)]);
            }
            return newRandom.ToString();
        }
        public static string GetRandChar(int strLength)
        {
            char[] constant = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            var newRandom = new StringBuilder(35);

            var rd = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));

            for (var i = 0; i < strLength; i++)
            {
                newRandom.Append(constant[rd.Next(35)]);
            }
            return newRandom.ToString();
        }

        /**
         * 将下划线大写方式命名的字符串转换为驼峰式。如果转换前的下划线大写方式命名的字符串为空，则返回空字符串。</br>
         * 例如：HELLO_WORLD->HelloWorld
         * @param name 转换前的下划线大写方式命名的字符串
         * @return 转换后的驼峰式命名的字符串
         */
        public static String ToCamelName(string name)
        {
            StringBuilder result = new StringBuilder();

            if (string.IsNullOrEmpty(name))
            {
                // 没必要转换
                return "";
            }
            else if (!name.Contains("_"))
            {
                // 不含下划线，仅将首字母小写
                return name.Substring(0, 1).ToLower() + name.Substring(1);
            }
            // 用下划线将原始字符串分割
            string[] camels = name.Split('_');

            foreach (var camel in camels)
            {
                // 跳过原始字符串中开头、结尾的下换线或双重下划线
                if (string.IsNullOrEmpty(camel))
                {
                    continue;
                }
                // 处理真正的驼峰片段
                if (result.Length == 0)
                {
                    // 第一个驼峰片段，全部字母都小写
                    result.Append(camel.ToLower());
                }
                else
                {
                    // 其他的驼峰片段，首字母大写
                    result.Append(camel.Substring(0, 1).ToUpper());
                    result.Append(camel.Substring(1).ToLower());
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// 生成随机字母字符串(数字字母混和)
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        /// <returns></returns>
        public static string GenerateCheckCode(int codeCount)
        {
            int rep = 0;
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        /// <summary>
        /// 生成随机字母字符串(仅字母，包含大小写)
        /// </summary>
        /// <param name="strLength">字符串长度</param>
        /// <returns></returns>
        public static string GetRandomCharacter(int strLength)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> 1)));
            for (int i = 0; i < strLength; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x61 + ((ushort)(num % 0x1a)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        public static string GetRandomCode(int strLength)
        {
            char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder();
            Random rd = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            for (int i = 0; i < strLength; i++)
            {
                newRandom.Append(constant[rd.Next(61)]);
            }
            return newRandom.ToString();
        }

        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        static Mutex _propMutex_funcGetTicks= new Mutex(); 
        public static long GetDateTimeTicks()
        {
            _propMutex_funcGetTicks.WaitOne();
            long r = DateTime.Now.Ticks;
            Thread.Sleep(1);
            _propMutex_funcGetTicks.ReleaseMutex();

            return r;
        }

        /// <summary>
        /// 获取毫秒，等同java【system.currenttimemills】
        /// </summary>
        /// <returns></returns>
        public static long GetTotalMilliseconds()
        {
            //用的 System.DateTime.UtcNow 而不是 System.DateTime.Now ，因为我们在东八区，如果用后面那种形式就会发现时间会和想象当中的差了8个小时。Java与C#时间转换到这里就彻底实现了。
            TimeSpan ts = new TimeSpan(System.DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
            return (long)ts.TotalMilliseconds;
        }

        public static string SubString(string s, int len)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            if (s.Length >= len)
            {
                return s.Substring(0, len);
            }

            return s;
        }


        /// <summary>
        /// 获取秒
        /// </summary>
        /// <returns></returns>
        public static long GetTotalSeconds()
        {
            //用的 System.DateTime.UtcNow 而不是 System.DateTime.Now ，因为我们在东八区，如果用后面那种形式就会发现时间会和想象当中的差了8个小时。Java与C#时间转换到这里就彻底实现了。
            TimeSpan ts = new TimeSpan(System.DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
            return (long)ts.TotalSeconds;
        }

    }
}
