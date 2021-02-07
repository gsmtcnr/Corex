using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Corex.CloudFile.Infrastructure
{
    public interface IUploadAsync : IUpload
    {
        void UploadAsyncFile(IFormFile data, string path = null);
        void UploadAsyncFile(IByteUpload data, string path = null);

        void UploadAsyncFiles(List<IFormFile> datas, string path = null);
        void UploadAsyncFiles(List<IByteUpload> datas, string path = null);
    }
}
