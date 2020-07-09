#region References

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using SchoolEnterpriseManageSys.Utilities.ExtensionHelper;

#endregion

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public class FTPClientHelper
    {
        #region Constants

        private const int BUFFLENGTH = 2048;

        #endregion

        #region Constructors and Destructors

        public FTPClientHelper(string remoteHost, string remotePath, string remoteUser, string remotePwd)
        {
            RemoteHost = remoteHost;
            RemotePath = remotePath;
            RemoteUser = remoteUser;
            RemotePwd = remotePwd;
        }

        #endregion

        #region Public Properties

        public string RemoteHost { get; private set; }
        public string RemotePath { get; private set; }
        public string RemotePwd { get; private set; }
        public string RemoteUser { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">
        /// 文件名
        /// </param>
        public void Delete(string fileName)
        {
            if (!IsExistFile(fileName))
            {
                throw new Exception(fileName + ",此文件不存在");
            }

            try
            {
                var uri = RemoteHost + "/" + RemotePath + "/" + fileName;
                FtpWebRequest reqFTP;

                reqFTP = (FtpWebRequest) WebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(RemoteUser, RemotePwd);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                var response = (FtpWebResponse) reqFTP.GetResponse();
                var sStatus = response.StatusDescription;
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("FTP删除文件异常:" + ex.Message);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">
        /// 保存路径
        /// </param>
        /// <param name="fileName">
        /// 服务器文件
        /// </param>
        public void Download(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            var uri = RemoteHost + "/" + RemotePath + "/" + fileName;
            try
            {
                var outputStream = new FileStream(filePath + @"\" + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest) WebRequest.Create(new Uri(uri));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(RemoteUser, RemotePwd);
                var response = (FtpWebResponse) reqFTP.GetResponse();
                var ftpStream = response.GetResponseStream();
                var cl = response.ContentLength;
                int readCount;
                var buffer = new byte[BUFFLENGTH];

                readCount = ftpStream.Read(buffer, 0, BUFFLENGTH);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, BUFFLENGTH);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("FTP文件下载异常:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取当前文件夹下文件列表
        /// </summary>
        /// <param name="mask">
        /// *.*全部,如:a则将返回为文件名为a开始的文件
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<string> GetFileList(string mask = "*.*")
        {
            var result = new List<string>();
            var uri = RemoteHost + "/" + RemotePath;
            FtpWebRequest ftp;
            ftp = (FtpWebRequest) WebRequest.Create(new Uri(uri));
            ftp.Credentials = new NetworkCredential(RemoteUser, RemotePwd);
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;
            var response = ftp.GetResponse();
            var reader = new StreamReader(response.GetResponseStream(), Encoding.Default);

            var line = reader.ReadLine();
            while (line != null)
            {
                if (!string.IsNullOrWhiteSpace(mask) && mask.Trim() != "*.*")
                {
                    var mask_ = mask.Substring(0, mask.IndexOf("*"));
                    if (line.Substring(0, mask_.Length) == mask_)
                    {
                        result.Add(line);
                    }
                }
                else
                {
                    result.Add(line);
                }

                line = reader.ReadLine();
            }

            reader.Close();
            response.Close();

            return result;
        }

        public bool IsExistFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(fileName);
            }

            return
                GetFileList("*.*")
                    .OpenSafe()
                    .Any(m => m.Trim().Equals(fileName.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filename">
        /// 本地文件详细路径
        /// </param>
        public void Upload(string filename)
        {
            if (IsExistFile(filename))
            {
                throw new Exception("FTP文件服务器已存在文件名为：" + filename + "文件");
            }

            var fileInfo = new FileInfo(filename);
            var uri = RemoteHost + "/" + RemotePath + "/" + fileInfo.Name;
            FtpWebRequest reqFTP;

            try
            {
                reqFTP = (FtpWebRequest) WebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(RemoteUser, RemotePwd);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                reqFTP.ContentLength = fileInfo.Length;

                var buff = new byte[BUFFLENGTH];
                int contentLength;
                var fs = fileInfo.OpenRead();

                var stream = reqFTP.GetRequestStream();
                contentLength = fs.Read(buff, 0, BUFFLENGTH);
                while (contentLength != 0)
                {
                    stream.Write(buff, 0, contentLength);
                    contentLength = fs.Read(buff, 0, BUFFLENGTH);
                }

                stream.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("FTP文件上传异常:" + ex.Message);
            }
        }

        #endregion
    }
}