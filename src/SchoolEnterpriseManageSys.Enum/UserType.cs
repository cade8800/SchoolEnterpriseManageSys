using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SchoolEnterpriseManageSys.Enum
{
    public enum UserType : int
    {
        /// <summary>
        /// 企业用户
        /// </summary>
        [Description("企业用户")]
        BusinessUser = 2,
        /// <summary>
        /// 校内用户
        /// </summary>
        [Description("校内用户")]
        OnCampusUser = 4
    }
}
