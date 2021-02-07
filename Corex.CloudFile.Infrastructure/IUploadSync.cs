using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Corex.CloudFile.Infrastructure
{
    public interface IUploadSync : IUpload
    {
        IUploadResult UploadFile(IFormFile data, string path = null);
        IUploadResult UploadFile(IByteUpload data, string path = null);
        List<IUploadResult> UploadFiles(List<IFormFile> datas, string path = null);
        List<IUploadResult> UploadFiles(List<IByteUpload> datas, string path = null);
    }
}
