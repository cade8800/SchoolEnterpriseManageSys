using Abp.Application.Services;
using Abp.Web.Models;
using SchoolEnterpriseManageSys.Project.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.Project
{
    public interface IProjectService : IApplicationService
    {
        [Authorize]
        GetProjectListOutput GetProjectList(GetProjectListInput input);
        [Authorize]
        GetProjectDetailOutput GetProjectDetail(GetProjectDetailInput input);

        [Authorize]
        void DeleteProject(Guid Id);



        [Authorize]
        void InsertAcademicAchievement(InsertAcademicAchievementInput input);
        [Authorize]
        void UpdateAcademicAchievement(AcademicAchievementDto input);
        [Authorize]
        void ImportAcademicAchievement(List<ImportAcademicAchievementInput> input);



        [Authorize]
        void InsertCampusBase(InsertCampusBaseInput input);
        [Authorize]
        void UpdateCampusBase(CampusBaseDto input);
        [Authorize]
        void ImportCampusBase(List<ImportCampusBaseInput> input);



        [Authorize]
        void InsertCoAuthoredBookOrCourse(InsertCoAuthoredBookOrCourseInput input);
        [Authorize]
        void UpdateCoAuthoredBookOrCourse(CoAuthoredBookOrCourseDto input);
        [Authorize]
        void ImportCoAuthoredBookOrCourse(List<ImportCoAuthoredBookOrCourseInput> input);



        [Authorize]
        void InsertJointlyEstablishedProfession(InsertJointlyEstablishedProfessionInput input);
        [Authorize]
        void UpdateJointlyEstablishedProfession(JointlyEstablishedProfessionDto input);




        [Authorize]
        void InsertOffCampusBase(InsertOffCampusBaseInput input);
        [Authorize]
        void UpdateOffCampusBase(OffCampusBaseDto input);
        [Authorize]
        void ImportOffCampusBase(List<ImportOffCampusBaseInput> input);



        [Authorize]
        void InsertOrderTraining(InsertOrderTrainingInput input);
        [Authorize]
        void UpdateOrderTraining(OrderTrainingDto input);
        [Authorize]
        void ImportOrderTraining(List<ImportOrderTrainingInput> input);



        [Authorize]
        void InsertSocialService(InsertSocialServiceInput input);
        [Authorize]
        void UpdateSocialService(SocialServiceDto input);
        [Authorize]
        void ImportSocialService(List<ImportSocialServiceInput> input);



        [Authorize]
        void InsertTeachingResearchFund(InsertTeachingResearchFundInput input);
        [Authorize]
        void UpdateTeachingResearchFund(TeachingResearchFundDto input);
        [Authorize]
        void ImportTeachingResearchFund(List<ImportTeachingResearchFundInput> input);


        [Authorize]
        GetGetSummaryOutput GetSummary();

        List<TimelineOutput> SelectSummary(SelectSummaryInput input);
    }
}
