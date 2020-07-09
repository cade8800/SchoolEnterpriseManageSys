using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class CooperationInput
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
        /// 校友会总数
        /// </summary>
        [Column("ALUMNI_ASSOCIATION_TOTAL")]
        public int AlumniAssociationTotal { get; set; }
        /// <summary>
        /// 境外校友会数量
        /// </summary>
        [Column("OVERSEAS_ASSOCIATION_COUNT")]
        public int OverseasAlumniAssociationCount { get; set; }
        /// <summary>
        /// 境内校友会数量
        /// </summary>
        [Column("DOMESTIC_ASSOCIATION_COUNT")]
        public int DomesticAlumniAssociationCount { get; set; }
        /// <summary>
        /// 合作机构总数
        /// </summary>
        [Column("COOPERATION_AGENCY_TOTAL")]
        public int CooperationAgencyTotal { get; set; }
        /// <summary>
        /// 学术机构数量
        /// </summary>
        [Column("ACADEMIC_AGENCY_COUNT")]
        public int AcademicAgencyCount { get; set; }
        /// <summary>
        /// 行业或企业机构数量
        /// </summary>
        [Column("ENTERPRISE_COUNT")]
        public int EnterpriseCount { get; set; }
        /// <summary>
        /// 地方政府数量
        /// </summary>
        [Column("LOCAL_GOVERNMENT_COUNT")]
        public int LocalGovernmentCount { get; set; }



        public List<FileDto> FileList { get; set; } = new List<FileDto>();
    }
}
