using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.File.Dto;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using SchoolEnterpriseManageSys.User;
using System.Web.Http;
using Abp.Timing;

namespace SchoolEnterpriseManageSys.File
{
    public class FileService : IFileService
    {
        private readonly IRepository<FileEntity, Guid> _fileRepository;
        private readonly IUserService _userService;
        private readonly IRepository<ProjectFileEntity, Guid> _projectFileRepository;
        private readonly IRepository<CollectFileEntity, Guid> _collectFileRepository;
        private readonly IRepository<EnterpriseFileEntity, Guid> _enterpriseFileRepository;
        private readonly IRepository<AssessmentFileEntity, Guid> _assessmentFileRepository;

        public FileService(
            IRepository<FileEntity, Guid> fileRepository,
            IUserService userService,
            IRepository<ProjectFileEntity, Guid> projectFileRepository,
            IRepository<CollectFileEntity, Guid> collectFileRepository,
            IRepository<EnterpriseFileEntity, Guid> enterpriseFileRepository,
            IRepository<AssessmentFileEntity, Guid> assessmentFileRepository
            )
        {
            _fileRepository = fileRepository;
            _userService = userService;
            _projectFileRepository = projectFileRepository;
            _collectFileRepository = collectFileRepository;
            _enterpriseFileRepository = enterpriseFileRepository;
            _assessmentFileRepository = assessmentFileRepository;
        }

        public void DeleteCollectFile([FromUri] CollectFileInput input)
        {
            var userClaim = _userService.UserClaim();
            var projectFile = _collectFileRepository.FirstOrDefault(t => t.CollectionItemId == input.CollectionItemId && t.FileId == input.FileId && t.IsDeleted == false);
            if (projectFile != null)
            {
                projectFile.IsDeleted = true;
                projectFile.UpdateTime = Clock.Now;
                projectFile.UpdateUserId = userClaim.UserId;
                _collectFileRepository.UpdateAsync(projectFile);

                var file = _fileRepository.FirstOrDefault(t => t.IsDeleted == false && t.Id == input.FileId);
                if (file != null)
                {
                    file.IsDeleted = true;
                    file.UpdateTime = Clock.Now;
                    file.UpdateUserId = userClaim.UserId;
                    _fileRepository.UpdateAsync(file);
                }
            }
        }


        public void DeleteFile(Guid fileId)
        {
            var userClaim = _userService.UserClaim();
            var file = _fileRepository.FirstOrDefault(t => t.Id == fileId && t.IsDeleted == false);
            if (file != null)
            {
                file.UpdateTime = Clock.Now;
                file.IsDeleted = true;
                file.UpdateUserId = userClaim.UserId;
                _fileRepository.UpdateAsync(file);
            }
        }

        public void DeleteProjectFile([FromUri] ProjectFileInput input)
        {
            var userClaim = _userService.UserClaim();
            var projectFile = _projectFileRepository.FirstOrDefault(t => t.ProjectId == input.ProjectId && t.FileId == input.FileId && t.IsDeleted == false);
            if (projectFile != null)
            {
                projectFile.IsDeleted = true;
                projectFile.UpdateTime = Clock.Now;
                projectFile.UpdateUserId = userClaim.UserId;
                _projectFileRepository.UpdateAsync(projectFile);

                var file = _fileRepository.FirstOrDefault(t => t.IsDeleted == false && t.Id == input.FileId);
                if (file != null)
                {
                    file.IsDeleted = true;
                    file.UpdateTime = Clock.Now;
                    file.UpdateUserId = userClaim.UserId;
                    _fileRepository.UpdateAsync(file);
                }
            }
        }

        public void InsertCollectFile(CollectFileInput input)
        {
            var userClaim = _userService.UserClaim();
            _collectFileRepository.InsertAsync(new CollectFileEntity
            {
                Id = Guid.NewGuid(),
                CreateUserId = userClaim.UserId,
                FileId = (Guid)input.FileId,
                CollectionItemId = (Guid)input.CollectionItemId
            });
        }


        public void InsertProjectFile(ProjectFileInput input)
        {
            var userClaim = _userService.UserClaim();
            _projectFileRepository.InsertAsync(new ProjectFileEntity
            {
                Id = Guid.NewGuid(),
                CreateUserId = userClaim.UserId,
                FileId = (Guid)input.FileId,
                ProjectId = (Guid)input.ProjectId
            });
        }

        public void UploadFile(UploadFileDto input)
        {
            var userClaim = _userService.UserClaim();
            _fileRepository.InsertAsync(new FileEntity
            {
                Id = input.Id,
                FileName = input.FileName,
                FileSuffix = input.FileSuffix,
                FileUrl = input.FileUrl,
                CreateUserId = userClaim.UserId
            });
        }




        public void InsertEnterpriseFile(EnterpriseFileInput input)
        {
            var userClaim = _userService.UserClaim();
            _enterpriseFileRepository.InsertAsync(new EnterpriseFileEntity
            {
                Id = Guid.NewGuid(),
                CreateUserId = userClaim.UserId,
                FileId = (Guid)input.FileId,
                UserId = input.EnterpriseId.HasValue ? input.EnterpriseId : userClaim.UserId
            });
        }

        public void DeleteEnterpriseFile([FromUri] EnterpriseFileInput input)
        {
            var userClaim = _userService.UserClaim();
            var enterpriseId = input.EnterpriseId.HasValue ? input.EnterpriseId : userClaim.UserId;
            var enterpriseFile = _enterpriseFileRepository.FirstOrDefault(t => t.UserId == enterpriseId && t.FileId == input.FileId && t.IsDeleted == false);
            if (enterpriseFile != null)
            {
                enterpriseFile.IsDeleted = true;
                enterpriseFile.UpdateTime = Clock.Now;
                enterpriseFile.UpdateUserId = userClaim.UserId;
                _enterpriseFileRepository.UpdateAsync(enterpriseFile);

                var file = _fileRepository.FirstOrDefault(t => t.IsDeleted == false && t.Id == input.FileId);
                if (file != null)
                {
                    file.IsDeleted = true;
                    file.UpdateTime = Clock.Now;
                    file.UpdateUserId = userClaim.UserId;
                    _fileRepository.UpdateAsync(file);
                }
            }
        }




        public void DeleteAssessmentFile([FromUri] AssessmentFileInput input)
        {
            var userClaim = _userService.UserClaim();
            var projectFile = _assessmentFileRepository.FirstOrDefault(t => t.DepartmentIndexId == input.DepartmentIndexId && t.FileId == input.FileId && t.IsDeleted == false);
            if (projectFile != null)
            {
                projectFile.IsDeleted = true;
                projectFile.UpdateTime = Clock.Now;
                projectFile.UpdateUserId = userClaim.UserId;
                _assessmentFileRepository.UpdateAsync(projectFile);

                var file = _fileRepository.FirstOrDefault(t => t.IsDeleted == false && t.Id == input.FileId);
                if (file != null)
                {
                    file.IsDeleted = true;
                    file.UpdateTime = Clock.Now;
                    file.UpdateUserId = userClaim.UserId;
                    _fileRepository.UpdateAsync(file);
                }
            }
        }

        public void InsertAssessmentFile(AssessmentFileInput input)
        {
            var userClaim = _userService.UserClaim();
            _assessmentFileRepository.InsertAsync(new AssessmentFileEntity
            {
                Id = Guid.NewGuid(),
                CreateUserId = userClaim.UserId,
                FileId = (Guid)input.FileId,
                DepartmentIndexId = input.DepartmentIndexId
            });
        }
    }
}
