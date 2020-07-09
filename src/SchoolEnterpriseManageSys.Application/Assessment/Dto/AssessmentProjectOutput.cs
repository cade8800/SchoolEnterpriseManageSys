using SchoolEnterpriseManageSys.Project.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class AssessmentProjectOutput
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Enum.ProjectType Type { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        [StringLength(32)]
        public string Number { get; set; }



        public string TypeText
        {
            get
            {
                return SchoolEnterpriseManageSys.Utilities.EnumHelper.EnumExtensions.GetDescription(Type);
            }
        }
        public List<ProjectFileOutput> FileList { get; set; } = new List<ProjectFileOutput>();
    }
}
