using Abp.Application.Services;
using Abp.Web.Models;
using SchoolEnterpriseManageSys.Assessment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.Assessment
{
    public interface IAssessmentService : IApplicationService
    {
        [Authorize]
        List<IndexDto> GetIndexs();

        [Authorize]
        void DeleteIndex([FromUri]DeleteIndexInput input);

        [Authorize]
        IndexDto GetIndex(GetIndexInput input);

        [Authorize]
        void EditIndex(IndexDto input);

        [Authorize]
        GetAssessmentsOutput GetAssessments(GetAssessmentsInput input);

        [Authorize]
        void EditAssessment(AssessmentDto input);

        [Authorize]
        AssessmentDto GetAssessment([FromUri]GetAssessmentInput input);

        [Authorize]
        void EditAssessmentDepartment(EditAssessmentDepartmentInput input);

        [Authorize]
        GetAssessmentDepartmentListOutput GetAssessmentDepartmentList(GetAssessmentDepartmentListInput input);


        [Authorize]
        GetAssessmentDepartmentIndexListOutput GetAssessmentDepartmentIndexList(GetAssessmentDepartmentIndexListInput input);

        [Authorize]
        GetAssessmentDepartmentProjectsOutput GetAssessmentDepartmentProjects(GetAssessmentDepartmentProjectsInput input);




        [Authorize]
        AssessmentDepartmentIndexOutput GetAssessmentDepartmentIndex(GetAssessmentDepartmentIndexInput input);

        [Authorize]
        void SelfEvaluation(SelfEvaluationInput input);

        [Authorize]
        void ExpertRating(ExpertRatingInput input);
    }
}
