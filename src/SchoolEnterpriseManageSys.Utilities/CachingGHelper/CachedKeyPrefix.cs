using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CachingGHelper
{
    public class CachedKeyPrefix
    {
        /// <summary>
        /// 根据活动Id缓存活动信息
        /// </summary>
        public static string MarketingActivtyByActivityId = "MarketingActivtyByActivityId";
        /// <summary>
        /// 根据规则Id缓存活动信息
        /// </summary>
        public static string MarketingActivtyByRuleId = "MarketingActivtyByRuleId";
        /// <summary>
        /// 根据规则Id缓存规则信息
        /// </summary>
        public static string MarketingRuleByRuleId = "MarketingRuleByRuleId";
        /// <summary>
        /// 缓存优惠方式信息
        /// </summary>
        public static string PromotionModuleInfo = "PromotionModuleInfo";
        /// <summary>
        /// 优惠规则基础字典信息
        /// </summary>
        public static string PromotionRuleInfo = "PromotionRuleInfo";

        /// <summary>
        /// 套餐优惠信息
        /// </summary>
        public static string PromotionPackageInfo = "PromotionPackageInfo";
    }
}
