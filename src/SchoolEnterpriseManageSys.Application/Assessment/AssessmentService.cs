using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Assessment.Dto;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using Abp.AutoMapper;
using System.Web.Http;
using SchoolEnterpriseManageSys.User;
using Abp.UI;
using Abp.Timing;

namespace SchoolEnterpriseManageSys.Assessment
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IRepository<AssessmentIndexEntity, Guid> _assessmentIndexRepository;
        private readonly IUserService _userService;
        private readonly IRepository<AssessmentEntity, Guid> _assessmentRepository;
        private readonly IRepository<AssessmentDepartmentEntity, Guid> _assessmentDepartmentRepository;
        private readonly IRepository<DepartmentEntity, Guid> _departmentRepository;
        private readonly IRepository<AssessmentDepartmentIndexEntity, Guid> _assessmentDepartmentIndexRepository;
        private readonly IRepository<AssessmentFileEntity, Guid> _assessmentFileRepository;
        private readonly IRepository<FileEntity, Guid> _fileRepository;
        private readonly IRepository<CooperationProjectEntity, Guid> _projectRepository;
        private readonly IRepository<ProjectFileEntity, Guid> _projectFileRepository;
        private readonly IRepository<UserEntity, Guid> _userRepository;

        public AssessmentService(
            IRepository<AssessmentIndexEntity, Guid> assessmentIndexRepository,
            IUserService userService,
            IRepository<AssessmentEntity, Guid> assessmentRepository,
            IRepository<AssessmentDepartmentEntity, Guid> assessmentDepartmentRepository,
            IRepository<DepartmentEntity, Guid> departmentRepository,
            IRepository<AssessmentDepartmentIndexEntity, Guid> assessmentDepartmentIndexRepository,
            IRepository<AssessmentFileEntity, Guid> assessmentFileRepository,
            IRepository<FileEntity, Guid> fileRepository,
            IRepository<CooperationProjectEntity, Guid> projectRepository,
            IRepository<ProjectFileEntity, Guid> projectFileRepository,
            IRepository<UserEntity, Guid> userRepository
            )
        {
            _assessmentIndexRepository = assessmentIndexRepository;
            _userService = userService;
            _assessmentRepository = assessmentRepository;
            _assessmentDepartmentRepository = assessmentDepartmentRepository;
            _departmentRepository = departmentRepository;
            _assessmentDepartmentIndexRepository = assessmentDepartmentIndexRepository;
            _assessmentFileRepository = assessmentFileRepository;
            _fileRepository = fileRepository;
            _projectRepository = projectRepository;
            _projectFileRepository = projectFileRepository;
            _userRepository = userRepository;
        }

        public void DeleteIndex([FromUri] DeleteIndexInput input)
        {
            var userClaim = _userService.UserClaim();
            var index = _assessmentIndexRepository.FirstOrDefault(t => t.Id == input.Id);
            if (index != null)
            {
                index.IsDeleted = !index.IsDeleted;
                index.UpdateTime = Clock.Now;
                index.UpdateUserId = userClaim.UserId;
                _assessmentIndexRepository.UpdateAsync(index);
            }
        }

        public void EditAssessment(AssessmentDto input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            var userClaim = _userService.UserClaim();
            if (input.Id.HasValue)
            {
                var assessment = _assessmentRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
                if (assessment != null)
                {
                    if (assessment.SchoolYear != input.SchoolYear && _assessmentRepository.Count(t => t.SchoolYear == input.SchoolYear) > 0)
                        throw new UserFriendlyException("学年[" + input.SchoolYear + "]的考核已经存在");

                    assessment.SchoolYear = input.SchoolYear;
                    assessment.StartTime = input.RangeTime[0].Date;
                    assessment.EndTime = input.RangeTime[1].Date;
                    assessment.Deadline = input.Deadline;
                    assessment.UpdateTime = Clock.Now;
                    assessment.UpdateUserId = userClaim.UserId;
                    _assessmentRepository.UpdateAsync(assessment);
                }
            }
            else
            {
                if (_assessmentRepository.Count(t => t.SchoolYear == input.SchoolYear) > 0)
                    throw new UserFriendlyException("学年[" + input.SchoolYear + "]的考核已经存在");

                var assessment = new AssessmentEntity
                {
                    Id = Guid.NewGuid(),
                    CreateUserId = userClaim.UserId,

                    SchoolYear = input.SchoolYear,
                    StartTime = input.RangeTime[0].Date,
                    EndTime = input.RangeTime[1].Date,
                    Deadline = input.Deadline
                };
                _assessmentRepository.InsertAsync(assessment);
                input.DepartmentList.ForEach(t =>
                {
                    _assessmentDepartmentRepository.InsertAsync(new AssessmentDepartmentEntity
                    {
                        Id = Guid.NewGuid(),
                        CreateUserId = userClaim.UserId,

                        AssessmentId = assessment.Id,
                        DepartmentId = t.Id
                    });
                });
            }
        }

        public void EditAssessmentDepartment(EditAssessmentDepartmentInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            var userClaim = _userService.UserClaim();

            var depAssessment = _assessmentDepartmentRepository.FirstOrDefault(t => t.DepartmentId == input.DepartmentId && t.AssessmentId == input.AssessmentId);
            if (depAssessment == null)
            {
                _assessmentDepartmentRepository.InsertAsync(new AssessmentDepartmentEntity
                {
                    AssessmentId = input.AssessmentId,
                    CreateUserId = userClaim.UserId,
                    DepartmentId = input.DepartmentId,
                    Id = Guid.NewGuid()
                });
            }
            else
            {
                depAssessment.IsDeleted = !depAssessment.IsDeleted;
                depAssessment.UpdateTime = Clock.Now;
                depAssessment.UpdateUserId = userClaim.UserId;
                _assessmentDepartmentRepository.UpdateAsync(depAssessment);
            }
        }

        public void EditIndex(IndexDto input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            var userClaim = _userService.UserClaim();
            if (input.Id.HasValue)
            {
                var assessment = _assessmentIndexRepository.FirstOrDefault(t => t.Id == input.Id);
                if (assessment != null)
                {
                    assessment.AssociatProjectType = input.AssociatProjectType;
                    assessment.CompleteStandard = input.CompleteStandard;
                    assessment.Content = input.Content;
                    assessment.IndexType = input.IndexType;
                    assessment.Remark = input.Remark;
                    assessment.StandardScore = input.StandardScore;

                    assessment.UpdateTime = Clock.Now;
                    assessment.UpdateUserId = userClaim.UserId;

                    _assessmentIndexRepository.UpdateAsync(assessment);
                }
            }
            else
            {
                _assessmentIndexRepository.InsertAsync(new AssessmentIndexEntity
                {
                    Id = Guid.NewGuid(),
                    CreateUserId = userClaim.UserId,

                    AssociatProjectType = input.AssociatProjectType,
                    CompleteStandard = input.CompleteStandard,
                    Content = input.Content,
                    IndexType = input.IndexType,
                    Remark = input.Remark,
                    StandardScore = input.StandardScore,
                    Sort = _assessmentIndexRepository.GetAll().OrderByDescending(t => t.Sort).Take(1).Select(t => t.Sort).FirstOrDefault() + 1
                });
            }
        }

        public void ExpertRating(ExpertRatingInput input)
        {
            var userClaim = _userService.UserClaim();
            if (input.Id.HasValue)
            {
                var depIndex = _assessmentDepartmentIndexRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
                if (depIndex != null)
                {
                    depIndex.ExpertRating = input.ExpertRating;
                    depIndex.ExpertRatingScore = input.ExpertRatingScore;
                    depIndex.UpdateTime = Clock.Now;
                    depIndex.UpdateUserId = userClaim.UserId;

                    _assessmentDepartmentIndexRepository.UpdateAsync(depIndex);
                }
            }
            else
            {
                _assessmentDepartmentIndexRepository.InsertAsync(new AssessmentDepartmentIndexEntity
                {
                    AssessmentDepartmentId = input.AssessmentDepartmentId,
                    AssessmentIndexId = input.AssessmentIndexId,
                    CreateUserId = userClaim.UserId,
                    ExpertRating = input.ExpertRating,
                    ExpertRatingScore = input.ExpertRatingScore,
                    Id = Guid.NewGuid()
                });
            }
        }

        public AssessmentDto GetAssessment([FromUri] GetAssessmentInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            var assessment = _assessmentRepository.FirstOrDefault(t => t.Id == input.Id).MapTo<AssessmentDto>();
            assessment.RangeTime = new DateTime[] { (DateTime)assessment.StartTime, (DateTime)assessment.EndTime };
            var depIdList = _assessmentDepartmentRepository.GetAll().Where(t => t.AssessmentId == input.Id && t.IsDeleted == false).Select(t => t.DepartmentId).ToList();
            depIdList.ForEach(t =>
            {
                if (t.HasValue)
                    assessment.DepartmentList.Add(new Department.Dto.DepartmentDto { Id = t });
            });
            return assessment;
        }

        public AssessmentDepartmentIndexOutput GetAssessmentDepartmentIndex(GetAssessmentDepartmentIndexInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator, Enum.RoleType.Department, Enum.RoleType.Expert });
            var userClaim = _userService.UserClaim();
            var output = _assessmentDepartmentIndexRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false).MapTo<AssessmentDepartmentIndexOutput>();
            var fileList = _assessmentFileRepository.GetAll().Where(t => t.DepartmentIndexId == input.Id && t.IsDeleted == false).ToList();
            fileList.ForEach(t =>
            {
                var file = _fileRepository.FirstOrDefault(f => f.Id == t.FileId && f.IsDeleted == false);
                output.FileList.Add(new AssessmentFileDto
                {
                    DepartmentIndexId = t.DepartmentIndexId,
                    FileId = t.FileId,
                    Id = t.Id,
                    Name = file?.FileName,
                    Uid = t.FileId,
                    Url = file?.FileUrl
                });
            });
            if (userClaim.Role == "department")
            {
                output.ExpertRating = string.Empty;
                output.ExpertRatingScore = 0;
            }
            var index = _assessmentIndexRepository.FirstOrDefault(t => t.Id == output.AssessmentIndexId && t.IsDeleted == false);
            if (index != null)
            {
                output.Sort = index.Sort;
                output.IndexType = index.IndexType;
                output.Content = index.Content;
                output.CompleteStandard = index.CompleteStandard;
                output.StandardScore = index.StandardScore;
                output.ProjectList = GetAssessmentDepartmentProjects(new GetAssessmentDepartmentProjectsInput { AssessmentDepartmentId = output.AssessmentDepartmentId }).ProjectList;
            }
            return output;
        }


        public GetAssessmentDepartmentProjectsOutput GetAssessmentDepartmentProjects(GetAssessmentDepartmentProjectsInput input)
        {
            GetAssessmentDepartmentProjectsOutput output = new GetAssessmentDepartmentProjectsOutput();
            var departmentAssessment = _assessmentDepartmentRepository.GetAll().Where(t => t.Id == input.AssessmentDepartmentId && t.IsDeleted == false).Select(t => new { t.DepartmentId, t.AssessmentId }).FirstOrDefault();

            if (departmentAssessment != null && departmentAssessment.AssessmentId.HasValue && departmentAssessment.DepartmentId.HasValue)
            {
                var assessment = _assessmentRepository.GetAll().Where(t => t.Id == departmentAssessment.AssessmentId && t.IsDeleted == false).Select(t => new { t.StartTime, t.EndTime }).FirstOrDefault();
                if (assessment != null && assessment.StartTime.HasValue && assessment.EndTime.HasValue)
                {
                    var projectList = _projectRepository.GetAll()
                        .Where(t =>
                        //t.Type == index.AssociatProjectType &&
                        t.DepartmentId == departmentAssessment.DepartmentId &&
                        //t.CreateTime >= assessment.StartTime &&
                        //t.CreateTime <= assessment.EndTime &&
                        t.StartTime >= assessment.StartTime &&
                        t.StartTime <= assessment.EndTime &&
                        t.IsDeleted == false)
                        .Select(t => new { t.Id, t.Number, t.Type })
                        .ToList();
                    projectList.ForEach(t =>
                    {
                        var assessmentProject = new AssessmentProjectOutput
                        {
                            Id = t.Id,
                            Number = t.Number,
                            Type = t.Type
                        };
                        var projectFileList = _projectFileRepository.GetAll().Where(f => f.ProjectId == t.Id && f.IsDeleted == false).ToList();
                        projectFileList.ForEach(f =>
                        {
                            var file = _fileRepository.FirstOrDefault(e => e.Id == f.FileId && e.IsDeleted == false);
                            assessmentProject.FileList.Add(new Project.Dto.ProjectFileOutput
                            {
                                Id = f.FileId,
                                ProjectId = f.ProjectId,
                                FileId = f.FileId,
                                Uid = file.Id,
                                Name = file.FileName,
                                Url = file.FileUrl
                            });
                        });
                        output.ProjectList.Add(assessmentProject);
                    });
                }
            }
            return output;
        }



        public GetAssessmentDepartmentIndexListOutput GetAssessmentDepartmentIndexList(GetAssessmentDepartmentIndexListInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator, Enum.RoleType.Department, Enum.RoleType.Expert });
            var userClaim = _userService.UserClaim();
            var output = new GetAssessmentDepartmentIndexListOutput();
            var indexList = _assessmentIndexRepository.GetAll().Where(t => t.IsDeleted == false).OrderBy(t => t.Sort).ToList();
            indexList.ForEach(t =>
            {
                output.AssessmentDepartmentIndexOutputs.Add(new AssessmentDepartmentIndexOutput
                {
                    IndexType = t.IndexType,
                    Sort = t.Sort,
                    Content = t.Content,
                    CompleteStandard = t.CompleteStandard,
                    StandardScore = t.StandardScore,
                    AssessmentIndexId = t.Id,
                    AssessmentDepartmentId = input.AssessmentDepartmentId
                });
            });
            output.AssessmentDepartmentIndexOutputs.ForEach(t =>
            {
                var depIndex = _assessmentDepartmentIndexRepository.FirstOrDefault(d => d.AssessmentIndexId == t.AssessmentIndexId && d.AssessmentDepartmentId == input.AssessmentDepartmentId && d.IsDeleted == false);
                if (depIndex != null)
                {
                    t.Id = depIndex.Id;
                    t.SelfEvaluationScore = depIndex.SelfEvaluationScore;
                    t.SelfEvaluation = depIndex.SelfEvaluation;
                    if (userClaim.Role != "department")
                    {
                        t.ExpertRatingScore = depIndex.ExpertRatingScore;
                        t.ExpertRating = depIndex.ExpertRating;
                    }

                    var fileList = _assessmentFileRepository.GetAll().Where(f => f.DepartmentIndexId == t.Id && f.IsDeleted == false).ToList();
                    fileList.ForEach(f =>
                    {
                        var file = _fileRepository.FirstOrDefault(s => s.Id == f.FileId && s.IsDeleted == false);
                        t.FileList.Add(new AssessmentFileDto
                        {
                            DepartmentIndexId = f.DepartmentIndexId,
                            FileId = f.FileId,
                            Id = f.Id,
                            Name = file?.FileName,
                            Uid = f.FileId,
                            Url = file?.FileUrl
                        });
                    });
                }
            });
            return output;
        }

        public GetAssessmentDepartmentListOutput GetAssessmentDepartmentList(GetAssessmentDepartmentListInput input)
        {
            var output = new GetAssessmentDepartmentListOutput
            {
                AssessmentDepartmentOutputs = _assessmentDepartmentRepository.GetAll().Where(t => t.AssessmentId == input.AssessmentId && t.IsDeleted == false).ToList().MapTo<List<AssessmentDepartmentOutput>>()
            };
            var totalScore = _assessmentIndexRepository.GetAll().Where(t => t.IsDeleted == false).Select(t => t.StandardScore).ToList().Sum();
            output.AssessmentDepartmentOutputs.ForEach(t =>
            {
                t.DepartmentName = _departmentRepository.FirstOrDefault(d => d.Id == t.DepartmentId)?.Name;
                t.TotalScore = totalScore;
                t.SinceScore = _assessmentDepartmentIndexRepository.GetAll().Where(s => s.AssessmentDepartmentId == t.Id && s.IsDeleted == false).Select(s => s.SelfEvaluationScore).ToList().Sum();
                t.ExpertScore = _assessmentDepartmentIndexRepository.GetAll().Where(s => s.AssessmentDepartmentId == t.Id && s.IsDeleted == false).Select(s => s.ExpertRatingScore).ToList().Sum();
            });
            return output;
        }

        public GetAssessmentsOutput GetAssessments(GetAssessmentsInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator, Enum.RoleType.Department, Enum.RoleType.Expert });
            var userClaim = _userService.UserClaim();
            var query = _assessmentRepository.GetAll();

            var depId = _userRepository.GetAll().Where(t => t.Id == userClaim.UserId && t.IsDeleted == false).Select(t => t.DepartmentId).FirstOrDefault();
            if (userClaim.Role == "department" && depId.HasValue)
            {
                var departmentAssessmentIds = _assessmentDepartmentRepository.GetAll().Where(t => t.DepartmentId == depId && t.IsDeleted == false).Select(t => t.AssessmentId).ToList();
                query = query.Where(t => departmentAssessmentIds.Contains(t.Id));
            }

            var output = new GetAssessmentsOutput
            {
                TotalCount = query.Count(),
                AssessmentList = query.OrderByDescending(t => t.SchoolYear).Skip(input.PageSize * (input.PageIndex - 1)).Take(input.PageSize).ToList().MapTo<List<AssessmentDto>>()
            };
            output.AssessmentList.ForEach(t =>
            {
                t.IsDeadline = Clock.Now > t.Deadline;
                t.RangeTime = new DateTime[] { (DateTime)t.StartTime, (DateTime)t.EndTime };

                if (userClaim.Role == "department" && depId.HasValue)
                {
                    t.AssessmentDepartmentId = _assessmentDepartmentRepository.GetAll().Where(d => d.DepartmentId == depId && d.AssessmentId == t.Id && d.IsDeleted == false).Select(d => d.Id).FirstOrDefault();
                }
            });
            return output;
        }

        public IndexDto GetIndex(GetIndexInput input)
        {
            return _assessmentIndexRepository.FirstOrDefault(t => t.Id == input.Id).MapTo<IndexDto>();
        }

        public List<IndexDto> GetIndexs()
        {
            return _assessmentIndexRepository.GetAll().Where(t => t.IsDeleted == false).OrderBy(t => t.Sort).ToList().MapTo<List<IndexDto>>();
        }

        public void SelfEvaluation(SelfEvaluationInput input)
        {
            var userClaim = _userService.UserClaim();
            if (input.Id.HasValue)
            {
                var depIndex = _assessmentDepartmentIndexRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
                if (depIndex != null)
                {
                    depIndex.SelfEvaluation = input.SelfEvaluation;
                    depIndex.SelfEvaluationScore = input.SelfEvaluationScore;
                    depIndex.UpdateTime = Clock.Now;
                    depIndex.UpdateUserId = userClaim.UserId;

                    _assessmentDepartmentIndexRepository.UpdateAsync(depIndex);
                }
            }
            else
            {
                var depIndex = new AssessmentDepartmentIndexEntity
                {
                    AssessmentDepartmentId = input.AssessmentDepartmentId,
                    AssessmentIndexId = input.AssessmentIndexId,
                    CreateUserId = userClaim.UserId,
                    SelfEvaluation = input.SelfEvaluation,
                    SelfEvaluationScore = input.SelfEvaluationScore,
                    Id = Guid.NewGuid()
                };
                _assessmentDepartmentIndexRepository.InsertAsync(depIndex);
                input.FileList.ForEach(t =>
                {
                    if (t.FileId.HasValue)
                    {
                        _assessmentFileRepository.InsertAsync(new AssessmentFileEntity
                        {
                            Id = Guid.NewGuid(),
                            FileId = t.FileId,
                            DepartmentIndexId = depIndex.Id,
                            CreateUserId = userClaim.UserId
                        });
                    }
                });
            }
        }
    }
}
