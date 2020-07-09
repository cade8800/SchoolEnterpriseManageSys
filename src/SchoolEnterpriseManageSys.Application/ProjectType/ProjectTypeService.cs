using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.ProjectType.Dto;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using Abp.AutoMapper;
using SchoolEnterpriseManageSys.User;
using SchoolEnterpriseManageSys.Enum;
using System.ComponentModel.DataAnnotations;
using Abp.Timing;

namespace SchoolEnterpriseManageSys.ProjectType
{
    public class ProjectTypeService : IProjectTypeService
    {
        private readonly IRepository<ProjectTypeEntity, Guid> _projectTypeRepository;
        private readonly IUserService _userService;
        public ProjectTypeService(IRepository<ProjectTypeEntity, Guid> projectTypeRepository, IUserService userService)
        {
            _projectTypeRepository = projectTypeRepository;
            _userService = userService;
        }
        public GetProjectTypeOutput Get()
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator, RoleType.Department });
            return new GetProjectTypeOutput
            {
                Types = _projectTypeRepository.GetAll().Where(t => t.IsDeleted == false && t.Type != Enum.ProjectType.JointlyEstablishedProfession).OrderBy(t => t.Type).MapTo<List<ProjectTypeDto>>()
            };
        }

        public ProjectTypeDto GetDetail([Required] Enum.ProjectType? type)
        {
            var projectType = _projectTypeRepository.FirstOrDefault(t => t.Type == type && t.IsDeleted == false);
            if (projectType == null) return new ProjectTypeDto();
            return projectType.MapTo<ProjectTypeDto>();
        }

        public void Update(ProjectTypeDto input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            var currentUser = _userService.UserClaim();
            var type = _projectTypeRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (type == null) throw new Exception("项目类型不存在");
            type.Instructions = input.Instructions;
            type.UploadFileDescription = input.UploadFileDescription;
            type.UpdateTime = Clock.Now;
            type.UpdateUserId = currentUser.UserId;
            _projectTypeRepository.UpdateAsync(type);
        }
    }
}
