using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SchoolEnterpriseManageSys.Enum
{
    public enum RoleType : int
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Administrator = 2,
        /// <summary>
        /// 企业用户
        /// </summary>
        [Description("企业用户")]
        Enterprise = 4,
        /// <summary>
        /// 专家
        /// </summary>
        [Description("专家")]
        Expert = 8,
        /// <summary>
        /// 系用户
        /// </summary>
        [Description("系用户")]
        Department = 16
    }
}
