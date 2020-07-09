using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class UpdateDepartmentCollectBaseInput
    {
        [Required]
        public Guid? Id { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        public string Remark { get; set; }



        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? BuiltUpTime { get; set; }
        /// <summary>
        /// 基地名称
        /// </summary>
        [StringLength(32)]
        public string BaseName { get; set; }

        /// <summary>
        /// 院系编号
        /// </summary>
        [StringLength(32)]
        public string DepartmentNumber { get; set; }
        /// <summary>
        /// 院系名称
        /// </summary>
        [StringLength(32)]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 专业编号
        /// </summary>
        [StringLength(32)]
        public string ScienceNumber { get; set; }
        /// <summary>
        /// 专业名称
        /// </summary>
        [StringLength(32)]
        public string ScienceName { get; set; }

        /// <summary>
        /// 是否创业实习基地
        /// </summary>
        public bool? IsPioneerBase { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(128)]
        public string Address { get; set; }
        /// <summary>
        /// 每次可接纳学生数（人）
        /// </summary>
        public int AccepteStudentAverage { get; set; }
        /// <summary>
        /// 当年接纳学生总数（人次）
        /// </summary>
        public int YearAccepteStudentTotal { get; set; }
    }
}
