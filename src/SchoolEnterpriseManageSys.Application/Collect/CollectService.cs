using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Collect.Dto;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using Abp.AutoMapper;
using Abp.UI;
using SchoolEnterpriseManageSys.User;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using Abp.Timing;

namespace SchoolEnterpriseManageSys.Collect
{
    public class CollectService : ICollectService
    {
        private readonly IRepository<CollectEntity, Guid> _collectRepository;
        private readonly IUserService _userService;
        private readonly IRepository<CollectDepartmentEntity, Guid> _collectDepartmentRepository;
        private readonly IRepository<CollectDepartmentCooperationEntity, Guid> _collectDepartmentCooperationRepository;
        private readonly IRepository<CollectDepartmentBaseEntity, Guid> _collectDepartmentBaseRepository;
        private readonly IRepository<UserEntity, Guid> _userRepository;
        private readonly IRepository<CollectFileEntity, Guid> _collectFileRepository;
        private readonly IRepository<FileEntity, Guid> _fileRepository;
        private readonly IRepository<DepartmentEntity, Guid> _departmentRepository;


        public CollectService(
            IRepository<CollectEntity, Guid> collectRepository,
            IUserService userService,
            IRepository<CollectDepartmentEntity, Guid> collectDepartmentRepository,
            IRepository<CollectDepartmentCooperationEntity, Guid> collectDepartmentCooperationRepository,
            IRepository<CollectDepartmentBaseEntity, Guid> collectDepartmentBaseRepository,
            IRepository<UserEntity, Guid> userRepository,
            IRepository<CollectFileEntity, Guid> collectFileRepository,
            IRepository<FileEntity, Guid> fileRepository,
            IRepository<DepartmentEntity, Guid> departmentRepository
            )
        {
            _collectRepository = collectRepository;
            _userService = userService;
            _collectDepartmentRepository = collectDepartmentRepository;
            _collectDepartmentCooperationRepository = collectDepartmentCooperationRepository;
            _collectDepartmentBaseRepository = collectDepartmentBaseRepository;
            _userRepository = userRepository;
            _collectFileRepository = collectFileRepository;
            _fileRepository = fileRepository;
            _departmentRepository = departmentRepository;
        }

        private void CheckSchoolYearIsExist(string schoolYear)
        {
            if (_collectRepository.Count(t => t.SchoolYear == schoolYear) > 0)
            {
                throw new UserFriendlyException("学年[" + schoolYear + "]的数据采集已经存在");
            }
        }

        public GetCollectOutput GetCollect(GetCollectInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator, Enum.RoleType.Department });
            var userClaim = _userService.UserClaim();
            var query = _collectRepository.GetAll().Where(t => t.IsDeleted == false);
            var output = new GetCollectOutput
            {
                TotalCount = query.Count(),
                CollectList = query.OrderByDescending(t => t.SchoolYear).Skip(input.PageSize * (input.PageIndex - 1)).Take(input.PageSize).ToList().MapTo<List<CollectOutput>>()
            };
            if (userClaim.Role == "department")
            {
                output.CollectList.ForEach(t =>
                {
                    var depId = _userRepository.GetAll().Where(d => d.IsDeleted == false && d.Id == userClaim.UserId).Select(d => d.DepartmentId).FirstOrDefault();
                    if (depId.HasValue)
                    {
                        t.CollectDepartmentId = _collectDepartmentRepository.GetAll().Where(c => c.IsDeleted == false && c.CollectionId == t.Id && c.DepartmentId == depId).FirstOrDefault()?.Id;
                    }
                });
            }
            return output;
        }

        public InsertCollectDto GetCollectDetail([FromUri] GetCollectDetailInput input)
        {
            var collect = _collectRepository.FirstOrDefault(t => t.Id == input.Id);
            if (collect == null) throw new UserFriendlyException("数据采集不存在");
            return collect.MapTo<InsertCollectDto>();
        }

        public void InsertCollect(InsertCollectDto input)
        {
            CheckSchoolYearIsExist(input.SchoolYear);
            var userClaim = _userService.UserClaim();
            var Collect = input.MapTo<CollectEntity>();
            Collect.Id = Guid.NewGuid();
            Collect.CreateUserId = userClaim.UserId;
            _collectRepository.InsertAsync(Collect);
        }


        public void UpdateCollect(InsertCollectDto input)
        {
            if (!input.Id.HasValue) throw new UserFriendlyException("数据采集不存在");
            var collect = _collectRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (collect == null) throw new UserFriendlyException("数据采集不存在");

            if (collect.SchoolYear != input.SchoolYear) CheckSchoolYearIsExist(input.SchoolYear);

            var userClaim = _userService.UserClaim();
            collect.UpdateTime = Clock.Now;
            collect.UpdateUserId = userClaim.UserId;

            collect.SchoolYear = input.SchoolYear;
            collect.DeadlineSubmission = (DateTime)input.DeadlineSubmission;
            collect.Description = input.Description;
            collect.BaseDescription = input.BaseDescription;
            collect.CooperationDescription = input.CooperationDescription;
            _collectRepository.UpdateAsync(collect);
        }


        public void InsertDepartmentCollect(InsertDepartmentCollectInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Department });
            var userClaim = _userService.UserClaim();

            var depId = _userRepository.FirstOrDefault(t => t.Id == userClaim.UserId)?.DepartmentId;
            if (!depId.HasValue) throw new UserFriendlyException("非系部用户");

            var depCollect = new CollectDepartmentEntity
            {
                CollectionId = (Guid)input.CollectionId,
                Id = Guid.NewGuid(),
                Remark = input.Remark,
                CreateUserId = userClaim.UserId,
                DepartmentId = (Guid)depId
            };
            _collectDepartmentRepository.InsertAsync(depCollect);

            input.BaseList.ForEach(b =>
            {
                var depCollectBase = b.MapTo<CollectDepartmentBaseEntity>();
                depCollectBase.Id = Guid.NewGuid();
                depCollectBase.CollectionDepartmentId = depCollect.Id;
                depCollectBase.CreateUserId = userClaim.UserId;
                _collectDepartmentBaseRepository.InsertAsync(depCollectBase);
                b.FileList.ForEach(t =>
                {
                    if (t.FileId.HasValue)
                        _collectFileRepository.InsertAsync(new CollectFileEntity
                        {
                            CollectionItemId = depCollectBase.Id,
                            CreateUserId = userClaim.UserId,
                            FileId = (Guid)t.FileId,
                            Id = Guid.NewGuid()
                        });
                });
            });

            var depCollectCooperation = input.Cooperation.MapTo<CollectDepartmentCooperationEntity>();
            depCollectCooperation.Id = Guid.NewGuid();
            depCollectCooperation.CollectionDepartmentId = depCollect.Id;
            depCollectCooperation.CreateUserId = userClaim.UserId;
            depCollectCooperation.CooperationAgencyTotal = depCollectCooperation.AcademicAgencyCount + depCollectCooperation.EnterpriseCount + depCollectCooperation.LocalGovernmentCount;
            _collectDepartmentCooperationRepository.InsertAsync(depCollectCooperation);
            input.Cooperation.FileList.ForEach(t =>
            {
                if (t.FileId.HasValue)
                    _collectFileRepository.InsertAsync(new CollectFileEntity
                    {
                        CollectionItemId = depCollectCooperation.Id,
                        CreateUserId = userClaim.UserId,
                        FileId = (Guid)t.FileId,
                        Id = Guid.NewGuid()
                    });
            });
        }

        public InsertDepartmentCollectInput GetDepartmentCollectDetail(GetDepartmentCollectDetailInput input)
        {

            var userClaim = _userService.UserClaim();
            var output = new InsertDepartmentCollectInput();

            var query = _collectDepartmentRepository.GetAll();

            if (input.DepartmentCollectId.HasValue)
            {
                query = query.Where(t => t.Id == input.DepartmentCollectId && t.IsDeleted == false);
            }
            else
            {
                var userDepId = _userRepository.FirstOrDefault(t => t.Id == userClaim.UserId && t.IsDeleted == false)?.DepartmentId;
                query = query.Where(t => t.CollectionId == input.CollectId && t.IsDeleted == false && t.DepartmentId == userDepId);
            }

            var depCollect = query.FirstOrDefault();

            if (depCollect != null)
            {
                output.Id = depCollect.Id;
                output.CollectionId = depCollect.CollectionId;
                output.Remark = depCollect.Remark;

                var collectBaseList = _collectDepartmentBaseRepository.GetAll().Where(t => t.CollectionDepartmentId == depCollect.Id && t.IsDeleted == false).ToList();
                collectBaseList.ForEach(collectBase =>
                {
                    var targetBase = collectBase.MapTo<BaseInput>();
                    var baseFileList = _collectFileRepository.GetAll().Where(t => t.CollectionItemId == collectBase.Id && t.IsDeleted == false).ToList();
                    baseFileList.ForEach(t =>
                    {
                        var file = _fileRepository.FirstOrDefault(f => f.Id == t.FileId && f.IsDeleted == false);
                        targetBase.FileList.Add(new FileDto
                        {
                            CollectionItemId = t.CollectionItemId,
                            FileId = t.FileId,
                            Id = t.Id,
                            Name = file?.FileName,
                            Uid = t.FileId,
                            Url = file?.FileUrl
                        });
                    });
                    output.BaseList.Add(targetBase);
                });


                var collectCooperation = _collectDepartmentCooperationRepository.FirstOrDefault(t => t.CollectionDepartmentId == depCollect.Id && t.IsDeleted == false);
                if (collectCooperation != null)
                {
                    output.Cooperation = collectCooperation.MapTo<CooperationInput>();
                    var fileList = _collectFileRepository.GetAll().Where(t => t.CollectionItemId == collectCooperation.Id && t.IsDeleted == false).ToList();
                    fileList.ForEach(t =>
                    {
                        var file = _fileRepository.FirstOrDefault(f => f.Id == t.FileId && f.IsDeleted == false);
                        output.Cooperation.FileList.Add(new FileDto
                        {
                            CollectionItemId = t.CollectionItemId,
                            FileId = t.FileId,
                            Id = t.Id,
                            Name = file?.FileName,
                            Uid = t.FileId,
                            Url = file?.FileUrl
                        });
                    });
                }
            }

            return output;
        }

        //public void UpdateDepartmentCollect(InsertDepartmentCollectInput input)
        //{
        //    var userClaim = _userService.UserClaim();
        //    if (!input.Id.HasValue || !input.Base.Id.HasValue || !input.Cooperation.Id.HasValue)
        //        throw new UserFriendlyException("系数据采集不存在");
        //    var depCollect = _collectDepartmentRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
        //    var depCollectBase = _collectDepartmentBaseRepository.FirstOrDefault(t => t.Id == input.Base.Id && t.IsDeleted == false);
        //    var depCollectCooperation = _collectDepartmentCooperationRepository.FirstOrDefault(t => t.Id == input.Cooperation.Id && t.IsDeleted == false);
        //    if (depCollect == null || depCollectBase == null || depCollectCooperation == null)
        //        throw new UserFriendlyException("系数据采集不存在");

        //    depCollect.Remark = input.Remark;
        //    depCollect.UpdateTime = Clock.Now;
        //    depCollect.UpdateUserId = userClaim.UserId;
        //    _collectDepartmentRepository.UpdateAsync(depCollect);

        //    depCollectBase.AccepteStudentAverage = input.Base.AccepteStudentAverage;
        //    depCollectBase.Address = input.Base.Address;
        //    depCollectBase.BaseName = input.Base.BaseName;
        //    depCollectBase.BuiltUpTime = input.Base.BuiltUpTime;
        //    depCollectBase.DepartmentName = input.Base.DepartmentName;
        //    depCollectBase.DepartmentNumber = input.Base.DepartmentNumber;
        //    depCollectBase.Remark = input.Base.Remark;
        //    depCollectBase.ScienceName = input.Base.ScienceName;
        //    depCollectBase.ScienceNumber = input.Base.ScienceNumber;
        //    depCollectBase.YearAccepteStudentTotal = input.Base.YearAccepteStudentTotal;
        //    depCollectBase.UpdateTime = Clock.Now;
        //    depCollectBase.UpdateUserId = userClaim.UserId;
        //    _collectDepartmentBaseRepository.UpdateAsync(depCollectBase);

        //    depCollectCooperation.AcademicAgencyCount = input.Cooperation.AcademicAgencyCount;
        //    depCollectCooperation.AlumniAssociationTotal = input.Cooperation.AlumniAssociationTotal;
        //    depCollectCooperation.DomesticAlumniAssociationCount = input.Cooperation.DomesticAlumniAssociationCount;
        //    depCollectCooperation.EnterpriseCount = input.Cooperation.EnterpriseCount;
        //    depCollectCooperation.LocalGovernmentCount = input.Cooperation.LocalGovernmentCount;
        //    depCollectCooperation.OverseasAlumniAssociationCount = input.Cooperation.OverseasAlumniAssociationCount;
        //    depCollectCooperation.Remark = input.Cooperation.Remark;
        //    depCollectCooperation.UpdateTime = Clock.Now;
        //    depCollectCooperation.UpdateUserId = userClaim.UserId;

        //    depCollectCooperation.CooperationAgencyTotal = depCollectCooperation.AcademicAgencyCount + depCollectCooperation.EnterpriseCount + depCollectCooperation.LocalGovernmentCount;

        //    _collectDepartmentCooperationRepository.UpdateAsync(depCollectCooperation);
        //}

        public GetDepartmentCollectListOutput GetDepartmentCollectList(GetDepartmentCollectListInput input)
        {
            var output = new GetDepartmentCollectListOutput();
            var depList = _departmentRepository.GetAll().Where(t => t.IsDeleted == false).ToList();
            depList.ForEach(t =>
            {
                var depCollect = _collectDepartmentRepository.FirstOrDefault(d => d.DepartmentId == t.Id && d.CollectionId == input.CollectId && t.IsDeleted == false);
                output.DepartmentCollectList.Add(new DepartmentCollectOutput
                {
                    DepartmentId = t.Id,
                    DepartmentName = t.Name,
                    CollectionId = depCollect?.CollectionId,
                    CreateTime = depCollect?.CreateTime,
                    DepartmentCollectId = depCollect?.Id,
                    Remark = depCollect?.Remark,
                    UpdateTime = depCollect?.UpdateTime
                });
            });
            return output;
        }

        public void UpdateDepartmentCollectCooperation(UpdateCooperationInput input)
        {
            var cooperation = _collectDepartmentCooperationRepository.Get((Guid)input.Id);
            var userClaim = _userService.UserClaim();

            cooperation.AcademicAgencyCount = input.AcademicAgencyCount;
            cooperation.EnterpriseCount = input.EnterpriseCount;
            cooperation.LocalGovernmentCount = input.LocalGovernmentCount;
            cooperation.CooperationAgencyTotal = input.AcademicAgencyCount + input.EnterpriseCount + input.LocalGovernmentCount;

            cooperation.UpdateTime = Clock.Now;
            cooperation.UpdateUserId = userClaim.UserId;

            _collectDepartmentCooperationRepository.UpdateAsync(cooperation);
        }

        public void DeleteDepartmentCollectBase(Guid id)
        {
            var targetBase = _collectDepartmentBaseRepository.FirstOrDefault(t => t.Id == id && t.IsDeleted == false);
            if (targetBase != null)
            {
                var userClaim = _userService.UserClaim();
                targetBase.IsDeleted = true;
                targetBase.UpdateTime = Clock.Now;
                targetBase.UpdateUserId = userClaim.UserId;
                _collectDepartmentBaseRepository.UpdateAsync(targetBase);
            }
        }

        public void UpdateDepartmentCollectBase(UpdateDepartmentCollectBaseInput input)
        {
            var targetBase = _collectDepartmentBaseRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
            if (targetBase != null)
            {
                var userClaim = _userService.UserClaim();
                targetBase.UpdateTime = Clock.Now;
                targetBase.UpdateUserId = userClaim.UserId;

                targetBase.AccepteStudentAverage = input.AccepteStudentAverage;
                targetBase.Address = input.Address;
                targetBase.BaseName = input.BaseName;
                targetBase.BuiltUpTime = input.BuiltUpTime;
                targetBase.DepartmentName = input.DepartmentName;
                targetBase.DepartmentNumber = input.DepartmentNumber;
                targetBase.IsPioneerBase = input.IsPioneerBase;
                targetBase.Remark = input.Remark;
                targetBase.ScienceName = input.ScienceName;
                targetBase.ScienceNumber = input.ScienceNumber;
                targetBase.YearAccepteStudentTotal = input.YearAccepteStudentTotal;

                _collectDepartmentBaseRepository.UpdateAsync(targetBase);
            }
        }

        public async Task<Guid> InsertDepartmentCollectBase(InsertDepartmentCollectBaseInput input)
        {
            CollectDepartmentBaseEntity newBase = input.MapTo<CollectDepartmentBaseEntity>();
            var userClaim = _userService.UserClaim();
            newBase.Id = Guid.NewGuid();
            newBase.CreateUserId = userClaim.UserId;
            return await _collectDepartmentBaseRepository.InsertAndGetIdAsync(newBase);
        }

        public List<FileDto> GetDepartmentCollecdtItemFileList(Guid id)
        {
            List<FileDto> output = new List<FileDto>();
            var fileIdList = _collectFileRepository.GetAll().Where(t => t.CollectionItemId == id && t.IsDeleted == false).Select(t => t.FileId).ToList();
            var fileList = _fileRepository.GetAll().Where(t => fileIdList.Contains(t.Id) && t.IsDeleted == false).ToList();
            fileList.ForEach(t =>
            {
                output.Add(new FileDto
                {
                    CollectionItemId = id,
                    FileId = t.Id,
                    Id = t.Id,
                    Name = t?.FileName,
                    Uid = t.Id,
                    Url = t?.FileUrl
                });
            });
            return output;
        }
    }
}
