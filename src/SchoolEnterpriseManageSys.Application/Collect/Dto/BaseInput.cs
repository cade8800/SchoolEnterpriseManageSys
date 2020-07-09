using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class BaseInput
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 系数据采集标识
        /// </summary>
        [Column("COLLECTION_DEPARTMENT_ID")]
        public Guid CollectionDepartmentId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK"), StringLength(512)]
        public string Remark { get; set; }



        /// <summary>
        /// 建立时间
        /// </summary>
        [Column("BUILT_UP_TIME")]
        public DateTime? BuiltUpTime { get; set; }
        /// <summary>
        /// 基地名称
        /// </summary>
        [Column("BASE_NAME"), StringLength(32)]
        public string BaseName { get; set; }

        /// <summary>
        /// 院系编号
        /// </summary>
        [Column("DEPARTMENT_NUMBER"), StringLength(32)]
        public string DepartmentNumber { get; set; }
        /// <summary>
        /// 院系名称
        /// </summary>
        [Column("DEPARTMENT_NAME"), StringLength(32)]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 专业编号
        /// </summary>
        [Column("SCIENCE_NUMBER"), StringLength(32)]
        public string ScienceNumber { get; set; }
        /// <summary>
        /// 专业名称
        /// </summary>
        [Column("SCIENCE_NAME"), StringLength(32)]
        public string ScienceName { get; set; }

        /// <summary>
        /// 是否创业实习基地
        /// </summary>
        [Column("IS_PIONEER_BASE")]
        public bool? IsPioneerBase { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Column("ADDRESS"), StringLength(128)]
        public string Address { get; set; }
        /// <summary>
        /// 每次可接纳学生数（人）
        /// </summary>
        [Column("ACCEPTE_STUDENT_AVERAGE")]
        public int AccepteStudentAverage { get; set; }
        /// <summary>
        /// 当年接纳学生总数（人次）
        /// </summary>
        [Column("YEAR_ACCEPTE_STUDENT_TOTAL")]
        public int YearAccepteStudentTotal { get; set; }








        public List<FileDto> FileList { get; set; } = new List<FileDto>();
    }
}
