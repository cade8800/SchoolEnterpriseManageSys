using AutoMapper;
using SchoolEnterpriseManageSys.Advisory.Dto;
using SchoolEnterpriseManageSys.Appointment.Dto;
using SchoolEnterpriseManageSys.Assessment.Dto;
using SchoolEnterpriseManageSys.Collect.Dto;
using SchoolEnterpriseManageSys.Enterprise.Dto;
using SchoolEnterpriseManageSys.Entities;
using SchoolEnterpriseManageSys.Menu.Dto;
using SchoolEnterpriseManageSys.Project.Dto;
using SchoolEnterpriseManageSys.ProjectType.Dto;
using SchoolEnterpriseManageSys.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<OSC_CAR, CarDetailOutput>();//.ForMember(t => t.AVATAR_ID, n => n.Ignore());
            CreateMap<UserEntity, LoginOutput>();
            CreateMap<MenuEntity, MenuOutput>();
            CreateMap<UserEntity, GetUserInfoOutput>();
            CreateMap<UserEntity, UserOutput>();
            CreateMap<InsertCampustUserInput, UserEntity>();

            CreateMap<CooperationProjectEntity, CoAuthoredBookOrCourseDto>();
            CreateMap<CooperationProjectEntity, JointlyEstablishedProfessionDto>();
            CreateMap<CooperationProjectEntity, OffCampusBaseDto>();
            CreateMap<CooperationProjectEntity, OrderTrainingDto>();
            CreateMap<CooperationProjectEntity, TeachingResearchFundDto>();
            CreateMap<ProjectTypeEntity, ProjectTypeDto>();
            CreateMap<CooperationProjectEntity, ProjectOutput>();
            CreateMap<CooperationProjectEntity, GetProjectDetailOutput>();
            CreateMap<InsertAcademicAchievementInput, CooperationProjectEntity>();
            CreateMap<InsertCampusBaseInput, CooperationProjectEntity>();
            CreateMap<InsertCoAuthoredBookOrCourseInput, CooperationProjectEntity>();
            CreateMap<InsertJointlyEstablishedProfessionInput, CooperationProjectEntity>();
            CreateMap<InsertOffCampusBaseInput, CooperationProjectEntity>();
            CreateMap<InsertOrderTrainingInput, CooperationProjectEntity>();
            CreateMap<InsertSocialServiceInput, CooperationProjectEntity>();
            CreateMap<InsertTeachingResearchFundInput, CooperationProjectEntity>();
            CreateMap<CooperationProjectEntity, BeAssociatedProjectDto>();

            CreateMap<CollectEntity, CollectOutput>();
            CreateMap<CollectEntity, InsertCollectDto>();
            CreateMap<InsertCollectDto, CollectEntity>();
            CreateMap<BaseInput, CollectDepartmentBaseEntity>();
            CreateMap<CooperationInput, CollectDepartmentCooperationEntity>();
            CreateMap<CollectDepartmentBaseEntity, BaseInput>();
            CreateMap<CollectDepartmentCooperationEntity, CooperationInput>();
            CreateMap<InsertDepartmentCollectBaseInput, CollectDepartmentBaseEntity>();

            CreateMap<EnterpriseInfoEntity, GetEnterpriseDetailOutput>();

            CreateMap<AdvisoryEntity, AdvisoryDto>();

            CreateMap<AppointmentEntity, AppointmentDto>();

            CreateMap<AssessmentEntity, AssessmentDto>();

            CreateMap<AssessmentDepartmentEntity, AssessmentDepartmentOutput>();
            CreateMap<AssessmentDepartmentIndexEntity, AssessmentDepartmentIndexOutput>();
        }
    }
}
