using Abp.Application.Services;
using Abp.Web.Models;
using SchoolEnterpriseManageSys.Collect.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Collect
{
    public interface ICollectService : IApplicationService
    {
        [Authorize]
        void InsertCollect(InsertCollectDto input);

        [Authorize]
        void UpdateCollect(InsertCollectDto input);

        [Authorize]
        GetCollectOutput GetCollect(GetCollectInput input);

        [Authorize]
        InsertCollectDto GetCollectDetail([FromUri]GetCollectDetailInput input);



        [Authorize]
        InsertDepartmentCollectInput GetDepartmentCollectDetail(GetDepartmentCollectDetailInput input);

        [Authorize]
        void InsertDepartmentCollect(InsertDepartmentCollectInput input);

        //[Authorize]
        //void UpdateDepartmentCollect(InsertDepartmentCollectInput input);

        [Authorize]
        void UpdateDepartmentCollectCooperation(UpdateCooperationInput input);


        [Authorize]
        void DeleteDepartmentCollectBase(Guid id);

        [Authorize]
        void UpdateDepartmentCollectBase(UpdateDepartmentCollectBaseInput input);


        [Authorize]
        Task<Guid> InsertDepartmentCollectBase(InsertDepartmentCollectBaseInput input);



        [Authorize]
        GetDepartmentCollectListOutput GetDepartmentCollectList(GetDepartmentCollectListInput input);

        List<FileDto> GetDepartmentCollecdtItemFileList(Guid id);
    }
}
