using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.BaseDto
{
    /// <summary>
    /// 分页output基类
    /// </summary>
    public class BasePagingOutput
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
