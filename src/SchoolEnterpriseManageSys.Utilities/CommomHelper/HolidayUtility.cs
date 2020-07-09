using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    /// <summary>
    /// 调用前，请确认已经添加日历配置文件
    /// </summary>
    public class HolidayUtility
    {
        private static List<HolidayItem> holidayItems = new List<HolidayItem>();

        //不休息的假日（例如情人节） 
        private static List<HolidayItem> noRestHolidayItems = new List<HolidayItem>();
        
        static HolidayUtility()
        {
            string path=Path.Combine( AppDomain.CurrentDomain.BaseDirectory,"Config/HolidayData.xml");
            XDocument doc = XDocument.Load(path);
            var items = doc.Descendants("Item");
            foreach (var item in items)
            {
                var isHoliday = item.Elements("IsHoliday").Select(o => o.Value).FirstOrDefault() == "true";
                var strDate = item.Elements("Date").Select(o => o.Value).FirstOrDefault();
                DateTime dt; 
                if (DateTime.TryParse(strDate, out dt))
                {
                    var holidayItem = new HolidayItem();
                    holidayItem.Name = item.Attributes("Name").Select(o => o.Value).FirstOrDefault();
                    holidayItem.Date = dt;
                    holidayItem.IsHoliday = isHoliday;
                    holidayItem.HolidayType = isHoliday ? 2 : 1;
                    holidayItems.Add(holidayItem);
                }
            }

            InitNoRestHoliday();

        }

        private static void InitNoRestHoliday()
        {

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/NoRestHolidayData.xml");
            if (File.Exists(path))
            {
                XDocument doc = XDocument.Load(path);
                var items = doc.Descendants("Item");
                foreach (var item in items)
                {
                    var strDate = item.Elements("Date").Select(o => o.Value).FirstOrDefault();
                    DateTime dt;
                    if (DateTime.TryParse(strDate, out dt))
                    {
                        var noRestHolidayItem = new HolidayItem();
                        noRestHolidayItem.Name = item.Attributes("Name").Select(o => o.Value).FirstOrDefault();
                        noRestHolidayItem.Date = dt;
                        noRestHolidayItem.IsHoliday = false;
                        noRestHolidayItem.HolidayType = 3;
                        noRestHolidayItems.Add(noRestHolidayItem);
                    }
                }
            }
        }
            

        public static List<HolidayItem> GetHolidayData()
        {
            return holidayItems;
        }

        public static bool IsHoliday(DateTime date)
        {
            return holidayItems.Any(o => o.Date.Date == date.Date && o.IsHoliday == true);
        }

        /// <summary>
        /// 添加不休息的假日，例如（情人节）
        /// </summary>
        public static List<HolidayItem> AppendNoRestHoliday(List<HolidayItem> holidayItems)
        {
            if (noRestHolidayItems.Count > 0)
            {
                List<HolidayItem> result = new List<HolidayItem>();
                result.AddRange(holidayItems);
                foreach (var noRestHolidayItem in noRestHolidayItems)
                {
                    var holidayItem = holidayItems.FirstOrDefault(o => o.Date == noRestHolidayItem.Date);
                    if (holidayItem == null)
                    {
                        result.Add(noRestHolidayItem);
                    }
                    else//如果和法定节假日重叠
                    {
                        HolidayItem data = new HolidayItem()
                        {
                            Date = holidayItem.Date,
                            IsHoliday = holidayItem.IsHoliday,
                            HolidayType = holidayItem.HolidayType,
                        };
                        //如果是法定节日，有些显示法定节日名称
                        data.Name = holidayItem.IsHoliday && !string.IsNullOrWhiteSpace(holidayItem.Name) ? holidayItem.Name : noRestHolidayItem.Name;
                        result.Add(data);
                    }
                }
                return result;
            }else
            {
                return holidayItems;
            }
        }


    }

    public class HolidayItem
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { set; get; }
        /// <summary>
        /// 是否假期
        /// </summary>
        public bool IsHoliday { set; get; }
        /// <summary>
        /// 假期类型 1班 2休 3不休息的假日（例如情人节） 
        /// </summary>
        public int HolidayType { set; get; }

    }

    [Serializable]
    public class ShareItem
    {
        public String Content { get; set; }
        public String Title { get; set; }
        public String Type { get; set; }

        public String Url { get; set; }

    }
}

