using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.BaseDto
{
    /// <summary>
    /// 分页input基类
    /// </summary>
    public class BasePagingInput
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="页码必须大于1")]
        public virtual int PageIndex { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
        [Required]
        [Range(1, 1000,ErrorMessage ="每页数量必须在1~1000之间")]
        public virtual int PageSize { get; set; }
    }
}
