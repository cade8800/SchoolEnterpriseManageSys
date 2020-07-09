using Abp.UI;
using Abp.Web.Models;
using SchoolEnterpriseManageSys.File;
using SchoolEnterpriseManageSys.File.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.Web.ApiControllers
{
    public class FileController : ApiController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }


        /// <summary>
        /// 图片上传 multipart/form-data
        /// </summary>
        /// <returns></returns>
        [Authorize, WrapResult]
        public async Task<List<UploadFileDto>> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new UserFriendlyException("上传格式不是multipart/form-data");

            //创建保存上传文件的物理路径
            var root = System.Web.Hosting.HostingEnvironment.MapPath(GetUploadSavePath());

            //如果路径不存在，创建路径  
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);

            var provider = new MultipartFormDataStreamProvider(root);

            //读取 MIME 多部分消息中的所有正文部分，并生成一组 HttpContent 实例作为结果
            await Request.Content.ReadAsMultipartAsync(provider);

            List<UploadFileDto> uploadFileResultList = new List<UploadFileDto>();

            foreach (var file in provider.FileData)
            {
                //获取上传文件名 这里获取含有双引号'" '
                string fileName = file.Headers.ContentDisposition.FileName.Trim('"');
                //获取上传文件后缀名
                string fileExt = fileName.Substring(fileName.LastIndexOf('.')).ToLower();

                FileInfo fileInfo = new FileInfo(file.LocalFileName);

                if (fileInfo.Length > 0 && fileInfo.Length <= GetUploadMaxByte())
                {
                    if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(GetUploadType().Split(','), fileExt.Substring(1).ToLower()) == -1)
                    {
                        fileInfo.Delete();
                        throw new UserFriendlyException("上传的文件格式不支持");
                    }
                    else
                    {
                        UploadFileDto uploadFile = new UploadFileDto
                        {
                            Id = Guid.NewGuid(),
                            FileName = fileName,
                            FileSuffix = fileExt
                        };
                        uploadFile.FileUrl = GetFullHost() + GetUploadSavePath() + uploadFile.Id.ToString() + fileExt;

                        fileInfo.MoveTo(Path.Combine(root, uploadFile.Id.ToString() + fileExt));
                        uploadFileResultList.Add(uploadFile);
                        _fileService.UploadFile(uploadFile);
                    }
                }
                else
                {
                    fileInfo.Delete();
                    throw new UserFriendlyException("上传文件的大小不符合");
                }
            }
            return uploadFileResultList;
        }

        /// <summary>
        /// 获取请求host
        /// </summary>
        /// <returns></returns>
        public string GetFullHost()
        {
            string domain = ConfigurationManager.AppSettings.Get("DomainName");
            return !string.IsNullOrWhiteSpace(domain) ?
             Url.Request.RequestUri.Scheme + "://" + domain : Url.Request.RequestUri.ToString().Replace(Url.Request.RequestUri.AbsolutePath, "");
        }

        private string GetUploadType()
        {
            return !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings.Get("UploadType")) ?
                ConfigurationManager.AppSettings.Get("UploadType") : "jpg,png,gif,pdf,doc,docx,xls,xlsx,ppt,pptx,txt,rar,zip";
        }
        private string GetUploadSavePath()
        {
            return !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings.Get("UploadSavePath")) ?
                ConfigurationManager.AppSettings.Get("UploadSavePath") : "/Resource/";
        }
        private int GetUploadMaxByte()
        {
            int.TryParse(ConfigurationManager.AppSettings.Get("UploadMaxByte"), out int UploadImgMaxByte);
            return UploadImgMaxByte > 0 ? UploadImgMaxByte : 20971520;
        }
    }
}
