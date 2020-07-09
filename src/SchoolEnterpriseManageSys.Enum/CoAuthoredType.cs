using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SchoolEnterpriseManageSys.Enum
{
    /// <summary>
    /// 共编教材或课程
    /// </summary>
    public enum CoAuthoredType : int
    {
        /// <summary>
        /// 教材
        /// </summary>
        [Description("教材")]
        Book = 2,
        /// <summary>
        /// 课程
        /// </summary>
        [Description("课程")]
        Course = 4
    }
}
