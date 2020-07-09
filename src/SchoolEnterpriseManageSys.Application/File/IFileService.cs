using SchoolEnterpriseManageSys.File.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Web.Models;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.File
{
    public interface IFileService : IApplicationService
    {
        [IgnoreWebApi]
        void UploadFile(UploadFileDto input);

        [Authorize]
        void DeleteFile(Guid fileId);

        [Authorize]
        void DeleteProjectFile([FromUri] ProjectFileInput input);

        [Authorize]
        void InsertProjectFile(ProjectFileInput input);


        [Authorize]
        void DeleteCollectFile([FromUri] CollectFileInput input);

        [Authorize]
        void InsertCollectFile(CollectFileInput input);



        [Authorize]
        void DeleteEnterpriseFile([FromUri] EnterpriseFileInput input);

        [Authorize]
        void InsertEnterpriseFile(EnterpriseFileInput input);


        [Authorize]
        void DeleteAssessmentFile([FromUri] AssessmentFileInput input);

        [Authorize]
        void InsertAssessmentFile(AssessmentFileInput input);
    }
}
