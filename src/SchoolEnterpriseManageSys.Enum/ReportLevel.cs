using System.ComponentModel;

namespace SchoolEnterpriseManageSys.Enum
{
    /// <summary>
    /// 申报级别
    /// </summary>
    public enum ReportLevel : int
    {
        /// <summary>
        /// 校
        /// </summary>
        [Description("校")]
        School = 2,
        /// <summary>
        /// 省
        /// </summary>
        [Description("省")]
        Province = 4,
        /// <summary>
        /// 部
        /// </summary>
        [Description("部")]
        Ministry = 8

    }
}
