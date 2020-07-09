using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Project.Dto;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using Abp.AutoMapper;
using SchoolEnterpriseManageSys.User;
using Abp.UI;
using SchoolEnterpriseManageSys.User.Dto;
using SchoolEnterpriseManageSys.Enum;
using Abp.Timing;

namespace SchoolEnterpriseManageSys.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<CooperationProjectEntity, Guid> _cooperationProjectRepository;
        private readonly IUserService _userService;
        private readonly IRepository<UserEntity, Guid> _userRepository;
        private readonly IRepository<ProjectFileEntity, Guid> _projectFileRepository;
        private readonly IRepository<ProjectTypeEntity, Guid> _projectTypeRepository;
        private readonly IRepository<DepartmentEntity, Guid> _departmentRepository;
        private readonly IRepository<FileEntity, Guid> _fileRepository;
        //private readonly IRepository<EnterpriseInfoEntity, Guid> _enterpriseInfoRepository;

        public ProjectService(
            IRepository<CooperationProjectEntity, Guid> cooperationProjectRepository,
            IUserService userService,
            IRepository<UserEntity, Guid> userRepository,
            IRepository<ProjectFileEntity, Guid> projectFileRepository,
            IRepository<ProjectTypeEntity, Guid> projectTypeRepository,
            IRepository<DepartmentEntity, Guid> departmentRepository,
            IRepository<FileEntity, Guid> fileRepository,
            IRepository<EnterpriseInfoEntity, Guid> enterpriseInfoRepository
            )
        {
            _cooperationProjectRepository = cooperationProjectRepository;
            _userService = userService;
            _userRepository = userRepository;
            _projectFileRepository = projectFileRepository;
            _projectTypeRepository = projectTypeRepository;
            _departmentRepository = departmentRepository;
            _fileRepository = fileRepository;
            //_enterpriseInfoRepository = enterpriseInfoRepository;
        }

        #region 帮助方法

        /// <summary>
        /// 获取系标识 系部用户从用户信息拿 其它用户无值报错有值直接返回
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="depIdInput"></param>
        /// <returns></returns>
        private Guid? GetDepId(UserClaimOutput userClaim, Guid? depIdInput)
        {
            if (userClaim.Role == "department")
            {
                var currentUser = _userRepository.FirstOrDefault(t => t.Id == userClaim.UserId && t.IsDeleted == false);
                return currentUser?.DepartmentId;
            }
            else
            {
                if (!depIdInput.HasValue) throw new UserFriendlyException("请选择系部");
                return depIdInput;
            }
        }
        /// <summary>
        /// 获取新编号值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private int GetNewProjectNumberValue(Enum.ProjectType type)
        {
            var maxNum = _cooperationProjectRepository.GetAll().Where(t => t.Type == type).OrderByDescending(t => t.NumberValue).FirstOrDefault()?.NumberValue;
            return maxNum.HasValue ? ((int)maxNum + 1) : 1;
        }
        /// <summary>
        /// 获取新编号
        /// </summary>
        /// <param name="type"></param>
        /// <param name="numberValue"></param>
        /// <returns></returns>
        private string GetNewProjectNumber(Enum.ProjectType type, int numberValue)
        {
            return type.ToString().Substring(0, 2).ToUpper() + numberValue.ToString().PadLeft(6, '0');
        }

        //private Guid? GetImportEnterpriseId(string enterpriseName)
        //{
        //    if (string.IsNullOrWhiteSpace(enterpriseName))
        //        return null;
        //    else
        //        return _enterpriseInfoRepository.GetAll().Where(t => t.FullName.Contains(enterpriseName) || t.NameAbbreviation.Contains(enterpriseName)).Take(1).Select(t => new { t.Id }).ToList().FirstOrDefault()?.Id;
        //}
        private Guid? GetImportDepartmentId(UserClaimOutput userClaim, string departmentName)
        {
            if (userClaim.Role == "department")
            {
                var currentUser = _userRepository.FirstOrDefault(t => t.Id == userClaim.UserId && t.IsDeleted == false);
                return currentUser?.DepartmentId;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(departmentName))
                    throw new UserFriendlyException("系名称不可为空");
                var depId = _departmentRepository.GetAll().Where(t => t.Name.Contains(departmentName)).Take(1).Select(t => new { t.Id }).ToList().FirstOrDefault()?.Id;
                if (depId.HasValue)
                    return depId;
                else
                    throw new UserFriendlyException("指定系[" + departmentName + "]不存在");
            }
        }

        private Guid? GetCurrentUserDepartmentId()
        {
            var userCliam = _userService.UserClaim();
            if (userCliam.Role == "department")
            {
                var depId = _userRepository.FirstOrDefault(t => t.Id == userCliam.UserId)?.DepartmentId;
                if (!depId.HasValue) throw new UserFriendlyException("系用户找不到对应系信息");
                return depId;
            }
            return null;
        }

        #endregion



        public GetProjectListOutput GetProjectList(GetProjectListInput input)
        {
            _userService.CheckUserPermissions(new List<RoleType> { RoleType.Administrator, RoleType.Department });
            var userCliam = _userService.UserClaim();
            var query = _cooperationProjectRepository.GetAll().Where(t => t.IsDeleted == false);
            if (userCliam.Role == "enterprise")
            {
                query = query.Where(t => t.EnterpriseId == userCliam.UserId);
            }

            var departmentId = GetCurrentUserDepartmentId();
            if (departmentId.HasValue)
                query = query.Where(t => t.DepartmentId == departmentId);

            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                //var enterpriseIds = _enterpriseInfoRepository.GetAll().Where(t => t.FullName.Contains(input.Keyword) || t.NameAbbreviation.Contains(input.Keyword)).Select(t => t.Id).ToList();
                var departmentIds = _departmentRepository.GetAll().Where(t => t.Name.Contains(input.Keyword)).Select(t => t.Id).ToList();
                //query = query.Where(t => t.Number.Contains(input.Keyword) || t.ProjectName.Contains(input.Keyword) || departmentIds.Contains((Guid)t.DepartmentId) || enterpriseIds.Contains((Guid)t.EnterpriseId));
                query = query.Where(t => t.Number.Contains(input.Keyword) || t.ProjectName.Contains(input.Keyword) || departmentIds.Contains((Guid)t.DepartmentId) || t.EnterpriseName.Contains(input.Keyword));
            }
            if (input.Type.HasValue)
                query = query.Where(t => t.Type == input.Type);
            var output = new GetProjectListOutput
            {
                TotalCount = query.Count(),
                ProjectList = query.OrderByDescending(t => t.CreateTime).OrderByDescending(t => t.Number).Skip(input.PageSize * (input.PageIndex - 1)).Take(input.PageSize).ToList().MapTo<List<ProjectOutput>>()
            };
            output.ProjectList.ForEach(t =>
            {
                t.DepartmentName = _departmentRepository.FirstOrDefault(d => d.Id == t.DepartmentId)?.Name;
                //var enterprise = _enterpriseInfoRepository.FirstOrDefault(e => e.Id == t.EnterpriseId);
                //t.EnterpriseName = string.IsNullOrEmpty(enterprise?.FullName) ? enterprise?.NameAbbreviation : enterprise?.FullName;

                if (t.Type == Enum.ProjectType.CampusBase || t.Type == Enum.ProjectType.OffCampusBase)
                {
                    if (t.EndTime.HasValue)
                    {
                        DateTime endDate = t.EndTime.Value.Date;
                        DateTime nowDate = Clock.Now.Date;
                        if (nowDate >= endDate)
                        {
                            t.OverdueShow = "已过期";
                        }
                        else
                        {
                            var remain = endDate.Subtract(nowDate).Days;
                            if (remain <= 30)
                            {
                                t.OverdueShow = remain + "天后到期";
                            }
                        }
                    }
                }


                var projectFiles = _projectFileRepository.GetAll().Where(f => f.ProjectId == t.Id && f.IsDeleted == false).ToList();

                projectFiles.ForEach(s =>
                {
                    var file = _fileRepository.FirstOrDefault(f => f.Id == s.FileId && f.IsDeleted == false);
                    if (file != null)
                    {
                        t.FileList.Add(new ProjectFileOutput
                        {
                            Id = s.Id,
                            ProjectId = s.ProjectId,
                            FileId = s.FileId,
                            Uid = file.Id,
                            Name = file.FileName,
                            Url = file.FileUrl
                        });
                    }
                });


                var beAssociatedQuery = _cooperationProjectRepository.GetAll().Where(p => p.RelateProjectId == t.Id && p.IsDeleted == false);
                if (departmentId.HasValue)
                    beAssociatedQuery = beAssociatedQuery.Where(p => p.DepartmentId == departmentId);
                var beAssociatedList = beAssociatedQuery.ToList();
                t.BeAssociatedProjectList = beAssociatedList.MapTo<List<BeAssociatedProjectDto>>();



            });
            return output;
        }

        public GetProjectDetailOutput GetProjectDetail(GetProjectDetailInput input)
        {
            var project = _cooperationProjectRepository.Get((Guid)input.Id);
            var output = project.MapTo<GetProjectDetailOutput>();
            var projectFiles = _projectFileRepository.GetAll().Where(t => t.ProjectId == project.Id && t.IsDeleted == false).ToList();
            if (output.RelateProjectId.HasValue)
            {
                var relateProject = _cooperationProjectRepository.FirstOrDefault(t => t.Id == output.RelateProjectId);
                output.RelateProjectName = relateProject?.ProjectName;
                var type = relateProject?.Type;
                if (type.HasValue)
                    output.RelateProjectTypeName = Utilities.EnumHelper.EnumExtensions.GetDescription(type);
            }
            projectFiles.ForEach(t =>
            {
                var file = _fileRepository.FirstOrDefault(f => f.Id == t.FileId && f.IsDeleted == false);
                if (file != null)
                {
                    output.FileList.Add(new ProjectFileOutput
                    {
                        Id = t.Id,
                        ProjectId = t.ProjectId,
                        FileId = t.FileId,
                        Uid = file.Id,
                        Name = file.FileName,
                        Url = file.FileUrl
                    });
                }
            });
            return output;
        }



        public void InsertAcademicAchievement(InsertAcademicAchievementInput input)
        {
            var userClaim = _userService.UserClaim();
            var project = input.MapTo<CooperationProjectEntity>();

            project.Type = Enum.ProjectType.AcademicAchievement;
            project.Id = Guid.NewGuid();
            project.DepartmentId = GetDepId(userClaim, input.DepartmentId);
            project.CreateUserId = userClaim.UserId;
            project.ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.AcademicAchievement && t.IsDeleted == false).Id;
            project.NumberValue = GetNewProjectNumberValue(Enum.ProjectType.AcademicAchievement);
            project.Number = GetNewProjectNumber(Enum.ProjectType.AcademicAchievement, project.NumberValue);

            input.FileIdList.ForEach(t =>
            {
                _projectFileRepository.InsertAsync(new ProjectFileEntity
                {
                    CreateUserId = userClaim.UserId,
                    FileId = t,
                    Id = Guid.NewGuid(),
                    ProjectId = project.Id
                });
            });
            _cooperationProjectRepository.InsertAsync(project);
        }
        public void UpdateAcademicAchievement(AcademicAchievementDto input)
        {
            var userClaim = _userService.UserClaim();
            if (!input.Id.HasValue) throw new UserFriendlyException("项目不存在");
            var project = _cooperationProjectRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (project == null) throw new UserFriendlyException("项目不存在");
            project.UpdateTime = Clock.Now;
            project.UpdateUserId = userClaim.UserId;
            project.DepartmentId = input.DepartmentId;
            project.ProjectName = input.ProjectName;
            project.Principal = input.Principal;
            project.PublicationName = input.PublicationName;
            project.PublicationsOrganizer = input.PublicationsOrganizer;
            project.ISSN = input.ISSN;
            project.CN = input.CN;
            project.StartTime = input.StartTime;
            project.RelateProjectId = input.RelateProjectId;
            project.Remark = input.Remark;
            _cooperationProjectRepository.UpdateAsync(project);
        }
        public void ImportAcademicAchievement(List<ImportAcademicAchievementInput> input)
        {
            _userService.CheckUserPermissions(new List<RoleType> { RoleType.Administrator, RoleType.Department });
            var userClaim = _userService.UserClaim();
            int numberValue = GetNewProjectNumberValue(Enum.ProjectType.AcademicAchievement) - 1;
            input.ForEach(item =>
            {
                numberValue++;
                CooperationProjectEntity project = new CooperationProjectEntity
                {
                    Type = Enum.ProjectType.AcademicAchievement,
                    Id = Guid.NewGuid(),
                    CreateUserId = userClaim.UserId,
                    ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.AcademicAchievement && t.IsDeleted == false).Id,
                    NumberValue = numberValue,

                    Principal = item.Principal,
                    ProjectName = item.ProjectName,
                    PublicationName = item.PublicationName,
                    PublicationsOrganizer = item.PublicationsOrganizer,
                    ISSN = item.ISSN,
                    CN = item.CN,
                    StartTime = item.StartTime
                };
                project.Number = GetNewProjectNumber(Enum.ProjectType.AcademicAchievement, project.NumberValue);

                project.DepartmentId = GetImportDepartmentId(userClaim, item.DepartmentName);

                _cooperationProjectRepository.InsertAsync(project);
            });
        }



        public void InsertCampusBase(InsertCampusBaseInput input)
        {
            var userClaim = _userService.UserClaim();
            var project = input.MapTo<CooperationProjectEntity>();

            project.Type = Enum.ProjectType.CampusBase;
            project.Id = Guid.NewGuid();
            project.CreateUserId = userClaim.UserId;
            project.ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.CampusBase && t.IsDeleted == false).Id;
            project.NumberValue = GetNewProjectNumberValue(Enum.ProjectType.CampusBase);
            project.Number = GetNewProjectNumber(Enum.ProjectType.CampusBase, project.NumberValue);

            project.DepartmentId = GetDepId(userClaim, input.DepartmentId);
            project.StartTime = input.RangeTime[0];
            project.EndTime = input.RangeTime[1];

            input.FileIdList.ForEach(t =>
            {
                _projectFileRepository.InsertAsync(new ProjectFileEntity
                {
                    CreateUserId = userClaim.UserId,
                    FileId = t,
                    Id = Guid.NewGuid(),
                    ProjectId = project.Id
                });
            });
            _cooperationProjectRepository.InsertAsync(project);
        }

        public void UpdateCampusBase(CampusBaseDto input)
        {
            var userClaim = _userService.UserClaim();
            if (!input.Id.HasValue) throw new UserFriendlyException("项目不存在");
            var project = _cooperationProjectRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (project == null) throw new UserFriendlyException("项目不存在");
            project.UpdateTime = Clock.Now;
            project.UpdateUserId = userClaim.UserId;

            project.DepartmentId = input.DepartmentId;
            project.ProjectName = input.ProjectName;
            project.Principal = input.Principal;
            project.RelateProjectId = input.RelateProjectId;
            project.Remark = input.Remark;
            project.Contact = input.Contact;
            project.EnterpriseName = input.EnterpriseName;
            project.ReportLevel = input.ReportLevel;
            project.Science = input.Science;
            project.StartTime = input.RangeTime[0];
            project.EndTime = input.RangeTime[1];

            _cooperationProjectRepository.UpdateAsync(project);
        }

        public void ImportCampusBase(List<ImportCampusBaseInput> input)
        {
            _userService.CheckUserPermissions(new List<RoleType> { RoleType.Administrator, RoleType.Department });
            var userClaim = _userService.UserClaim();
            int numberValue = GetNewProjectNumberValue(Enum.ProjectType.CampusBase) - 1;
            input.ForEach(item =>
            {
                numberValue++;
                CooperationProjectEntity project = new CooperationProjectEntity
                {
                    Type = Enum.ProjectType.CampusBase,
                    Id = Guid.NewGuid(),
                    CreateUserId = userClaim.UserId,
                    ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.CampusBase && t.IsDeleted == false).Id,
                    NumberValue = numberValue,

                    Science = item.Science,
                    Principal = item.Principal,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    ReportLevel = item.ReportLevel,

                    EnterpriseName = item.EnterpriseName
                };
                project.Number = GetNewProjectNumber(Enum.ProjectType.CampusBase, project.NumberValue);

                //project.EnterpriseId = GetImportEnterpriseId(item.EnterpriseName);
                project.DepartmentId = GetImportDepartmentId(userClaim, item.DepartmentName);

                _cooperationProjectRepository.InsertAsync(project);
            });
        }



        public void InsertCoAuthoredBookOrCourse(InsertCoAuthoredBookOrCourseInput input)
        {
            var userClaim = _userService.UserClaim();
            var project = input.MapTo<CooperationProjectEntity>();

            project.Type = Enum.ProjectType.CoAuthoredBookOrCourse;
            project.Id = Guid.NewGuid();
            project.CreateUserId = userClaim.UserId;
            project.ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.CoAuthoredBookOrCourse && t.IsDeleted == false).Id;
            project.NumberValue = GetNewProjectNumberValue(Enum.ProjectType.CoAuthoredBookOrCourse);
            project.Number = GetNewProjectNumber(Enum.ProjectType.CoAuthoredBookOrCourse, project.NumberValue);

            project.DepartmentId = GetDepId(userClaim, input.DepartmentId);


            input.FileIdList.ForEach(t =>
            {
                _projectFileRepository.InsertAsync(new ProjectFileEntity
                {
                    CreateUserId = userClaim.UserId,
                    FileId = t,
                    Id = Guid.NewGuid(),
                    ProjectId = project.Id
                });
            });
            _cooperationProjectRepository.InsertAsync(project);
        }

        public void UpdateCoAuthoredBookOrCourse(CoAuthoredBookOrCourseDto input)
        {
            var userClaim = _userService.UserClaim();
            if (!input.Id.HasValue) throw new UserFriendlyException("项目不存在");
            var project = _cooperationProjectRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (project == null) throw new UserFriendlyException("项目不存在");
            project.UpdateTime = Clock.Now;
            project.UpdateUserId = userClaim.UserId;

            project.DepartmentId = input.DepartmentId;
            project.ProjectName = input.ProjectName;
            project.Principal = input.Principal;
            project.RelateProjectId = input.RelateProjectId;
            project.Remark = input.Remark;
            project.EnterpriseName = input.EnterpriseName;
            project.Science = input.Science;
            project.ISBN = input.ISBN;
            project.StartTime = input.StartTime;
            project.CoAuthoredType = input.CoAuthoredType;

            _cooperationProjectRepository.UpdateAsync(project);
        }

        public void ImportCoAuthoredBookOrCourse(List<ImportCoAuthoredBookOrCourseInput> input)
        {
            _userService.CheckUserPermissions(new List<RoleType> { RoleType.Administrator, RoleType.Department });
            var userClaim = _userService.UserClaim();
            int numberValue = GetNewProjectNumberValue(Enum.ProjectType.CoAuthoredBookOrCourse) - 1;
            input.ForEach(item =>
            {
                numberValue++;
                CooperationProjectEntity project = new CooperationProjectEntity
                {
                    Type = Enum.ProjectType.CoAuthoredBookOrCourse,
                    Id = Guid.NewGuid(),
                    CreateUserId = userClaim.UserId,
                    ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.CoAuthoredBookOrCourse && t.IsDeleted == false).Id,
                    NumberValue = numberValue,

                    CoAuthoredType = item.CoAuthoredType,
                    ProjectName = item.ProjectName,
                    Principal = item.Principal,
                    Science = item.Science,
                    StartTime = item.StartTime,
                    ISBN = item.ISBN,

                    EnterpriseName = item.EnterpriseName
                };
                project.Number = GetNewProjectNumber(Enum.ProjectType.CoAuthoredBookOrCourse, project.NumberValue);

                //project.EnterpriseId = GetImportEnterpriseId(item.EnterpriseName);
                project.DepartmentId = GetImportDepartmentId(userClaim, item.DepartmentName);

                _cooperationProjectRepository.InsertAsync(project);
            });
        }



        public void InsertJointlyEstablishedProfession(InsertJointlyEstablishedProfessionInput input)
        {
            var userClaim = _userService.UserClaim();
            var project = input.MapTo<CooperationProjectEntity>();

            project.Type = Enum.ProjectType.JointlyEstablishedProfession;
            project.Id = Guid.NewGuid();
            project.CreateUserId = userClaim.UserId;
            project.ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.JointlyEstablishedProfession && t.IsDeleted == false).Id;
            project.NumberValue = GetNewProjectNumberValue(Enum.ProjectType.JointlyEstablishedProfession);
            project.Number = GetNewProjectNumber(Enum.ProjectType.JointlyEstablishedProfession, project.NumberValue);
            project.DepartmentId = GetDepId(userClaim, input.DepartmentId);

            input.FileIdList.ForEach(t =>
            {
                _projectFileRepository.InsertAsync(new ProjectFileEntity
                {
                    CreateUserId = userClaim.UserId,
                    FileId = t,
                    Id = Guid.NewGuid(),
                    ProjectId = project.Id
                });
            });
            _cooperationProjectRepository.InsertAsync(project);
        }

        public void UpdateJointlyEstablishedProfession(JointlyEstablishedProfessionDto input)
        {
            var userClaim = _userService.UserClaim();
            if (!input.Id.HasValue) throw new UserFriendlyException("项目不存在");
            var project = _cooperationProjectRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (project == null) throw new UserFriendlyException("项目不存在");
            project.UpdateTime = Clock.Now;
            project.UpdateUserId = userClaim.UserId;

            project.DepartmentId = input.DepartmentId;
            project.ProjectName = input.ProjectName;
            project.Principal = input.Principal;
            project.RelateProjectId = input.RelateProjectId;
            project.Remark = input.Remark;
            project.EnterpriseName = input.EnterpriseName;
            project.StartTime = input.StartTime;

            _cooperationProjectRepository.UpdateAsync(project);
        }



        public void InsertOffCampusBase(InsertOffCampusBaseInput input)
        {
            var userClaim = _userService.UserClaim();
            var project = input.MapTo<CooperationProjectEntity>();

            project.Type = Enum.ProjectType.OffCampusBase;
            project.Id = Guid.NewGuid();
            project.CreateUserId = userClaim.UserId;
            project.ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.OffCampusBase && t.IsDeleted == false).Id;
            project.NumberValue = GetNewProjectNumberValue(Enum.ProjectType.OffCampusBase);
            project.Number = GetNewProjectNumber(Enum.ProjectType.OffCampusBase, project.NumberValue);

            project.DepartmentId = GetDepId(userClaim, input.DepartmentId);
            project.StartTime = input.RangeTime[0];
            project.EndTime = input.RangeTime[1];

            input.FileIdList.ForEach(t =>
            {
                _projectFileRepository.InsertAsync(new ProjectFileEntity
                {
                    CreateUserId = userClaim.UserId,
                    FileId = t,
                    Id = Guid.NewGuid(),
                    ProjectId = project.Id
                });
            });
            _cooperationProjectRepository.InsertAsync(project);
        }

        public void UpdateOffCampusBase(OffCampusBaseDto input)
        {
            var userClaim = _userService.UserClaim();
            if (!input.Id.HasValue) throw new UserFriendlyException("项目不存在");
            var project = _cooperationProjectRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (project == null) throw new UserFriendlyException("项目不存在");
            project.UpdateTime = Clock.Now;
            project.UpdateUserId = userClaim.UserId;

            project.DepartmentId = input.DepartmentId;
            project.Principal = input.Principal;
            project.RelateProjectId = input.RelateProjectId;
            project.Remark = input.Remark;
            project.EnterpriseName = input.EnterpriseName;
            project.ReportLevel = input.ReportLevel;
            project.Science = input.Science;
            project.StartTime = input.RangeTime[0];
            project.EndTime = input.RangeTime[1];

            _cooperationProjectRepository.UpdateAsync(project);
        }
        public void ImportOffCampusBase(List<ImportOffCampusBaseInput> input)
        {
            _userService.CheckUserPermissions(new List<RoleType> { RoleType.Administrator, RoleType.Department });
            var userClaim = _userService.UserClaim();
            int numberValue = GetNewProjectNumberValue(Enum.ProjectType.OffCampusBase) - 1;
            input.ForEach(item =>
            {
                numberValue++;
                CooperationProjectEntity project = new CooperationProjectEntity
                {
                    Type = Enum.ProjectType.OffCampusBase,
                    Id = Guid.NewGuid(),
                    CreateUserId = userClaim.UserId,
                    ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.OffCampusBase && t.IsDeleted == false).Id,
                    NumberValue = numberValue,

                    Science = item.Science,
                    Principal = item.Principal,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    ReportLevel = item.ReportLevel,

                    EnterpriseName = item.EnterpriseName
                };
                project.Number = GetNewProjectNumber(Enum.ProjectType.OffCampusBase, project.NumberValue);

                //project.EnterpriseId = GetImportEnterpriseId(item.EnterpriseName);
                project.DepartmentId = GetImportDepartmentId(userClaim, item.DepartmentName);

                _cooperationProjectRepository.InsertAsync(project);
            });
        }



        public void InsertOrderTraining(InsertOrderTrainingInput input)
        {
            var userClaim = _userService.UserClaim();
            var project = input.MapTo<CooperationProjectEntity>();

            project.Type = Enum.ProjectType.OrderTraining;
            project.Id = Guid.NewGuid();
            project.CreateUserId = userClaim.UserId;
            project.ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.OrderTraining && t.IsDeleted == false).Id;
            project.NumberValue = GetNewProjectNumberValue(Enum.ProjectType.OrderTraining);
            project.Number = GetNewProjectNumber(Enum.ProjectType.OrderTraining, project.NumberValue);
            project.DepartmentId = GetDepId(userClaim, input.DepartmentId);

            input.FileIdList.ForEach(t =>
            {
                _projectFileRepository.InsertAsync(new ProjectFileEntity
                {
                    CreateUserId = userClaim.UserId,
                    FileId = t,
                    Id = Guid.NewGuid(),
                    ProjectId = project.Id
                });
            });
            _cooperationProjectRepository.InsertAsync(project);
        }

        public void UpdateOrderTraining(OrderTrainingDto input)
        {
            var userClaim = _userService.UserClaim();
            if (!input.Id.HasValue) throw new UserFriendlyException("项目不存在");
            var project = _cooperationProjectRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (project == null) throw new UserFriendlyException("项目不存在");
            project.UpdateTime = Clock.Now;
            project.UpdateUserId = userClaim.UserId;

            project.DepartmentId = input.DepartmentId;
            project.Science = input.Science;
            project.ClassName = input.ClassName;
            project.ClassStudentCount = input.ClassStudentCount;
            project.StartTime = input.StartTime;
            project.EnterpriseName = input.EnterpriseName;
            project.RelateProjectId = input.RelateProjectId;
            project.Remark = input.Remark;

            _cooperationProjectRepository.UpdateAsync(project);
        }
        public void ImportOrderTraining(List<ImportOrderTrainingInput> input)
        {
            _userService.CheckUserPermissions(new List<RoleType> { RoleType.Administrator, RoleType.Department });
            var userClaim = _userService.UserClaim();
            int numberValue = GetNewProjectNumberValue(Enum.ProjectType.OrderTraining) - 1;
            input.ForEach(item =>
            {
                numberValue++;
                CooperationProjectEntity project = new CooperationProjectEntity
                {
                    Type = Enum.ProjectType.OrderTraining,
                    Id = Guid.NewGuid(),
                    CreateUserId = userClaim.UserId,
                    ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.OrderTraining && t.IsDeleted == false).Id,
                    NumberValue = numberValue,

                    Science = item.Science,
                    ClassName = item.ClassName,
                    ClassStudentCount = item.ClassStudentCount,
                    StartTime = item.StartTime,

                    EnterpriseName = item.EnterpriseName
                };
                project.Number = GetNewProjectNumber(Enum.ProjectType.OrderTraining, project.NumberValue);

                //project.EnterpriseId = GetImportEnterpriseId(item.EnterpriseName);
                project.DepartmentId = GetImportDepartmentId(userClaim, item.DepartmentName);

                _cooperationProjectRepository.InsertAsync(project);
            });
        }



        public void InsertSocialService(InsertSocialServiceInput input)
        {
            var userClaim = _userService.UserClaim();
            var project = input.MapTo<CooperationProjectEntity>();

            project.Type = Enum.ProjectType.SocialService;
            project.Id = Guid.NewGuid();
            project.CreateUserId = userClaim.UserId;
            project.ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.SocialService && t.IsDeleted == false).Id;
            project.NumberValue = GetNewProjectNumberValue(Enum.ProjectType.SocialService);
            project.Number = GetNewProjectNumber(Enum.ProjectType.SocialService, project.NumberValue);
            project.DepartmentId = GetDepId(userClaim, input.DepartmentId);

            input.FileIdList.ForEach(t =>
            {
                _projectFileRepository.InsertAsync(new ProjectFileEntity
                {
                    CreateUserId = userClaim.UserId,
                    FileId = t,
                    Id = Guid.NewGuid(),
                    ProjectId = project.Id
                });
            });
            _cooperationProjectRepository.InsertAsync(project);
        }

        public void UpdateSocialService(SocialServiceDto input)
        {
            var userClaim = _userService.UserClaim();
            if (!input.Id.HasValue) throw new UserFriendlyException("项目不存在");
            var project = _cooperationProjectRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (project == null) throw new UserFriendlyException("项目不存在");
            project.UpdateTime = Clock.Now;
            project.UpdateUserId = userClaim.UserId;

            project.DepartmentId = input.DepartmentId;
            project.Principal = input.Principal;
            project.EnterpriseName = input.EnterpriseName;
            project.StartTime = input.StartTime;
            project.Amount = input.Amount;
            project.RelateProjectId = input.RelateProjectId;
            project.Remark = input.Remark;

            _cooperationProjectRepository.UpdateAsync(project);
        }
        public void ImportSocialService(List<ImportSocialServiceInput> input)
        {
            _userService.CheckUserPermissions(new List<RoleType> { RoleType.Administrator, RoleType.Department });
            var userClaim = _userService.UserClaim();
            int numberValue = GetNewProjectNumberValue(Enum.ProjectType.SocialService) - 1;
            input.ForEach(item =>
            {
                numberValue++;
                CooperationProjectEntity project = new CooperationProjectEntity
                {
                    Type = Enum.ProjectType.SocialService,
                    Id = Guid.NewGuid(),
                    CreateUserId = userClaim.UserId,
                    ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.SocialService && t.IsDeleted == false).Id,
                    NumberValue = numberValue,

                    Principal = item.Principal,
                    StartTime = item.StartTime,
                    Amount = item.Amount,

                    EnterpriseName = item.EnterpriseName
                };
                project.Number = GetNewProjectNumber(Enum.ProjectType.SocialService, project.NumberValue);

                //project.EnterpriseId = GetImportEnterpriseId(item.EnterpriseName);
                project.DepartmentId = GetImportDepartmentId(userClaim, item.DepartmentName);

                _cooperationProjectRepository.InsertAsync(project);
            });
        }



        public void InsertTeachingResearchFund(InsertTeachingResearchFundInput input)
        {
            var userClaim = _userService.UserClaim();
            var project = input.MapTo<CooperationProjectEntity>();

            project.Type = Enum.ProjectType.TeachingResearchFund;
            project.Id = Guid.NewGuid();
            project.CreateUserId = userClaim.UserId;
            project.ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.TeachingResearchFund && t.IsDeleted == false).Id;
            project.NumberValue = GetNewProjectNumberValue(Enum.ProjectType.TeachingResearchFund);
            project.Number = GetNewProjectNumber(Enum.ProjectType.TeachingResearchFund, project.NumberValue);
            project.DepartmentId = GetDepId(userClaim, input.DepartmentId);

            project.StartTime = input.RangeTime[0];
            project.EndTime = input.RangeTime[1];

            input.FileIdList.ForEach(t =>
            {
                _projectFileRepository.InsertAsync(new ProjectFileEntity
                {
                    CreateUserId = userClaim.UserId,
                    FileId = t,
                    Id = Guid.NewGuid(),
                    ProjectId = project.Id
                });
            });
            _cooperationProjectRepository.InsertAsync(project);
        }

        public void UpdateTeachingResearchFund(TeachingResearchFundDto input)
        {
            var userClaim = _userService.UserClaim();
            if (!input.Id.HasValue) throw new UserFriendlyException("项目不存在");
            var project = _cooperationProjectRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (project == null) throw new UserFriendlyException("项目不存在");
            project.UpdateTime = Clock.Now;
            project.UpdateUserId = userClaim.UserId;

            project.DepartmentId = input.DepartmentId;
            project.ProjectName = input.ProjectName;
            project.Amount = input.Amount;
            project.EnterpriseName = input.EnterpriseName;
            project.StartTime = input.RangeTime[0];
            project.EndTime = input.RangeTime[1];
            project.RelateProjectId = input.RelateProjectId;
            project.Remark = input.Remark;

            _cooperationProjectRepository.UpdateAsync(project);
        }
        public void ImportTeachingResearchFund(List<ImportTeachingResearchFundInput> input)
        {
            _userService.CheckUserPermissions(new List<RoleType> { RoleType.Administrator, RoleType.Department });
            var userClaim = _userService.UserClaim();
            int numberValue = GetNewProjectNumberValue(Enum.ProjectType.TeachingResearchFund) - 1;
            input.ForEach(item =>
            {
                numberValue++;
                CooperationProjectEntity project = new CooperationProjectEntity
                {
                    Type = Enum.ProjectType.TeachingResearchFund,
                    Id = Guid.NewGuid(),
                    CreateUserId = userClaim.UserId,
                    ProjectTypeId = _projectTypeRepository.FirstOrDefault(t => t.Type == Enum.ProjectType.TeachingResearchFund && t.IsDeleted == false).Id,
                    NumberValue = numberValue,

                    ProjectName = item.ProjectName,
                    Amount = item.Amount,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,

                    EnterpriseName = item.EnterpriseName
                };
                project.Number = GetNewProjectNumber(Enum.ProjectType.TeachingResearchFund, project.NumberValue);

                //project.EnterpriseId = GetImportEnterpriseId(item.EnterpriseName);
                project.DepartmentId = GetImportDepartmentId(userClaim, item.DepartmentName);

                _cooperationProjectRepository.InsertAsync(project);
            });
        }



        public GetGetSummaryOutput GetSummary()
        {
            var output = new GetGetSummaryOutput
            {
                RankingList = _cooperationProjectRepository.GetAll().Where(t => t.IsDeleted == false && t.Type != Enum.ProjectType.JointlyEstablishedProfession).GroupBy(t => t.Type).Select(t => new RankingOutput
                {
                    Type = t.Select(s => s.Type).FirstOrDefault(),
                    Total = t.Count()
                }).ToList().OrderByDescending(t => t.Total).ToList()
            };

            DateTime now = Clock.Now;
            for (int i = 11; i >= 0; i--)
            {
                DateTime target = now.AddMonths(-i);

                DateTime startDate = new DateTime(target.Year, target.Month, 1);
                DateTime endDate = (new DateTime(target.Year, target.Month, 1, 23, 59, 59)).AddMonths(1).AddDays(-1);

                output.TimelineList.Add(new TimelineOutput
                {
                    X = target.Year + "年" + target.Month + "月",
                    Y = _cooperationProjectRepository.Count(t => t.IsDeleted == false && t.CreateTime >= startDate && t.CreateTime <= endDate && t.Type != Enum.ProjectType.JointlyEstablishedProfession)
                });
            }

            return output;
        }

        public List<TimelineOutput> SelectSummary(SelectSummaryInput input)
        {
            List<TimelineOutput> output = new List<TimelineOutput>();
            var typeList = _projectTypeRepository.GetAll().Where(t => t.Type != Enum.ProjectType.JointlyEstablishedProfession).OrderBy(t => t.Type).Select(t => new { t.Id, t.ProjectTypeName }).ToList();

            typeList.ForEach(t =>
            {
                TimelineOutput timeLine = new TimelineOutput
                {
                    X = t.ProjectTypeName
                };
                var query = _cooperationProjectRepository.GetAll().Where(p => p.IsDeleted == false && p.ProjectTypeId == t.Id);
                if (input.DepartmentId.HasValue)
                    query = query.Where(p => p.DepartmentId == input.DepartmentId);
                if (input.StartTime.HasValue && input.EndTime.HasValue)
                {
                    DateTime startDate = input.StartTime.Value.Date;
                    DateTime endDate = input.EndTime.Value.Date;
                    query = query.Where(p => p.CreateTime >= startDate && p.CreateTime <= endDate);
                }
                timeLine.Y = query.Count();

                output.Add(timeLine);
            });

            return output;
        }


        public void DeleteProject(Guid Id)
        {
            _userService.CheckUserPermissions(new List<RoleType> { RoleType.Administrator, RoleType.Department });
            var project = _cooperationProjectRepository.FirstOrDefault(t => t.Id == Id && t.IsDeleted == false);
            if (project == null)
                throw new UserFriendlyException("指定档案不存在，请刷新后重试");

            var userClaim = _userService.UserClaim();
            project.IsDeleted = true;
            project.UpdateTime = Clock.Now;
            project.UpdateUserId = userClaim.UserId;
            _cooperationProjectRepository.UpdateAsync(project);
        }
    }
}
