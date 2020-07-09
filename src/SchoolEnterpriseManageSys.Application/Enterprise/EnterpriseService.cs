using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Enterprise.Dto;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using Abp.AutoMapper;
using SchoolEnterpriseManageSys.User;
using Abp.UI;
using Abp.Timing;

namespace SchoolEnterpriseManageSys.Enterprise
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IRepository<EnterpriseInfoEntity, Guid> _enterpriseInfoRepository;
        private readonly UserService _userService;
        private readonly IRepository<EnterpriseFileEntity, Guid> _enterpriseFileRepository;
        private readonly IRepository<FileEntity, Guid> _fileRepository;

        public EnterpriseService(
            IRepository<EnterpriseInfoEntity, Guid> enterpriseInfoRepository,
            UserService userService,
            IRepository<EnterpriseFileEntity, Guid> enterpriseFileRepository,
            IRepository<FileEntity, Guid> fileRepository
            )
        {
            _enterpriseInfoRepository = enterpriseInfoRepository;
            _userService = userService;
            _enterpriseFileRepository = enterpriseFileRepository;
            _fileRepository = fileRepository;
        }

        public void Edit(EnterpriseOutput input)
        {
            var userClaim = _userService.UserClaim();
            if (input.Id.HasValue)
            {
                var enterprise = _enterpriseInfoRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
                if (enterprise != null)
                {
                    if (enterprise.FullName != input.FullName)
                    {
                        if (_enterpriseInfoRepository.Count(t => t.FullName.Contains(input.FullName) && t.IsDeleted == false) > 0)
                        {
                            throw new UserFriendlyException("企业名称已被占用，请与管理员取得联系");
                        }
                    }
                    enterprise.FullName = input.FullName;
                    enterprise.NameAbbreviation = input.NameAbbreviation;
                    enterprise.LegalRepresentative = input.LegalRepresentative;
                    enterprise.FixedTelephone = input.FixedTelephone;
                    enterprise.ContactName = input.ContactName;
                    enterprise.Scale = input.Scale;
                    enterprise.CompanyProfile = input.CompanyProfile;

                    enterprise.UpdateTime = Clock.Now;
                    enterprise.UpdateUserId = userClaim.UserId;

                    _enterpriseInfoRepository.UpdateAsync(enterprise);
                }
            }
            else
            {
                if (_enterpriseInfoRepository.Count(t => t.FullName.Contains(input.FullName) && t.IsDeleted == false) > 0)
                {
                    throw new UserFriendlyException("企业名称已被占用，请与管理员取得联系");
                }
                _enterpriseInfoRepository.InsertAsync(new EnterpriseInfoEntity
                {
                    Id = userClaim.UserId,
                    FullName = input.FullName,
                    NameAbbreviation = input.NameAbbreviation,
                    LegalRepresentative = input.LegalRepresentative,
                    FixedTelephone = input.FixedTelephone,
                    ContactName = input.ContactName,
                    Scale = input.Scale,
                    CompanyProfile = input.CompanyProfile,

                    CreateUserId = userClaim.UserId
                });
            }
        }

        public GetEnterpriseOutput Get(GetEnterpriseInput input)
        {
            var query = _enterpriseInfoRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                string key = input.Keyword.Trim();
                query = query.Where(t => t.FullName.Contains(key) || t.NameAbbreviation.Contains(key) || t.ContactName.Contains(key) || t.LegalRepresentative.Contains(key));
            }
            var output = new GetEnterpriseOutput
            {
                TotalCount = query.Count(),
                Enterprises = query.OrderBy(t => t.FullName).Skip(input.PageSize * (input.PageIndex - 1)).Take(input.PageSize).ToList().MapTo<List<GetEnterpriseDetailOutput>>()
            };
            if (input.IsWithFileInfo.HasValue && input.IsWithFileInfo == true)
            {
                output.Enterprises.ForEach(t =>
                {
                    var fileList = _enterpriseFileRepository.GetAll().Where(f => f.UserId == t.Id && f.IsDeleted == false).ToList();
                    fileList.ForEach(f =>
                    {
                        var file = _fileRepository.FirstOrDefault(d => d.Id == f.FileId && f.IsDeleted == false);
                        t.FileList.Add(new EnterpriseFileDto
                        {
                            FileId = f.FileId,
                            Id = f.Id,
                            Uid = (Guid)f.FileId,
                            Name = file.FileName,
                            Url = file.FileUrl
                        });
                    });

                });
            }
            return output;
        }

        public GetEnterpriseDetailOutput GetDetail(GetEnterpriseDetailInput input)
        {
            var output = new GetEnterpriseDetailOutput();
            var targetId = input.Id.HasValue ? input.Id : _userService.UserClaim().UserId;
            var enterprise = _enterpriseInfoRepository.FirstOrDefault(t => t.Id == targetId && t.IsDeleted == false);

            if (enterprise != null)
            {
                output = enterprise.MapTo<GetEnterpriseDetailOutput>();
                var fileList = _enterpriseFileRepository.GetAll().Where(t => t.UserId == enterprise.Id && t.IsDeleted == false).ToList();
                fileList.ForEach(t =>
                {
                    var file = _fileRepository.FirstOrDefault(f => f.Id == t.FileId && f.IsDeleted == false);
                    if (file != null)
                    {
                        output.FileList.Add(new EnterpriseFileDto
                        {
                            Id = t.Id,
                            FileId = t.FileId,
                            Uid = file.Id,
                            Name = file.FileName,
                            Url = file.FileUrl
                        });
                    }
                });
            }

            return output;
        }
    }
}
