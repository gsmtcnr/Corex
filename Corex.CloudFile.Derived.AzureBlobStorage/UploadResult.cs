using Corex.CloudFile.Infrastructure;
using Corex.Model.Infrastructure;

namespace Corex.CloudFile.Derived.AzureBlobStorage
{
    public class UploadResult : BaseResultModel, IUploadResult
    {
        public UploadResult()
        {
            IsSuccess = true;
        }
    }
}
