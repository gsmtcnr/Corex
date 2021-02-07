using Corex.CloudFile.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;

namespace Corex.CloudFile.Derived.AzureBlobStorage
{
    public abstract class BaseAzureBlobStorage : IUploadAsync, IUploadSync
    {
        public abstract string GetConnectionString();
        private readonly CloudBlobContainer _cloudBlobContainer;
        public BaseAzureBlobStorage()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(GetConnectionString());
            string accountName = cloudStorageAccount.Credentials.AccountName;
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            _cloudBlobContainer = cloudBlobClient.GetContainerReference(accountName);
            //create a container if it is not already exists
            if (_cloudBlobContainer.CreateIfNotExists())
            {
                _cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                { PublicAccess = BlobContainerPublicAccessType.Blob });
            }
        }

        public virtual void UploadAsyncFile(IFormFile data, string path = null)
        {
            string blobName = string.Format("{0}{1}", (path != null ? string.Format("{0}/", path) : string.Empty), data.Name);
            CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(blobName);
            cloudBlockBlob.Properties.ContentType = data.ContentType;
            cloudBlockBlob.UploadFromStreamAsync(data.OpenReadStream());
        }

        public virtual void UploadAsyncFile(IByteUpload data, string path = null)
        {
            string blobName = string.Format("{0}{1}", (path != null ? string.Format("{0}/", path) : string.Empty), data.FileName);
            CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(blobName);
            cloudBlockBlob.Properties.ContentType = data.FileExtension;
            cloudBlockBlob.UploadFromByteArrayAsync(data.FileData, 0, data.FileData.Length);
        }

        public virtual void UploadAsyncFiles(List<IFormFile> datas, string path = null)
        {
            foreach (var data in datas)
            {
                UploadAsyncFile(data, path);
            }
        }

        public virtual void UploadAsyncFiles(List<IByteUpload> datas, string path = null)
        {
            foreach (var data in datas)
            {
                UploadAsyncFile(data, path);
            }
        }

        public virtual IUploadResult UploadFile(IFormFile data, string path = null)
        {
            UploadResult uploadResult = new UploadResult();
            try
            {
                string blobName = string.Format("{0}{1}", (path != null ? string.Format("{0}/", path) : string.Empty), data.Name);
                CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(blobName);
                cloudBlockBlob.Properties.ContentType = data.ContentType;
                cloudBlockBlob.UploadFromStream(data.OpenReadStream());
            }
            catch (Exception ex)
            {
                uploadResult.IsSuccess = false;
                uploadResult.Message = ex.ToString();

            }
            return uploadResult;
        }

        public virtual IUploadResult UploadFile(IByteUpload data, string path = null)
        {
            UploadResult uploadResult = new UploadResult();
            try
            {
                string blobName = string.Format("{0}{1}", (path != null ? string.Format("{0}/", path) : string.Empty), data.FileName);
                CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(blobName);
                cloudBlockBlob.Properties.ContentType = data.FileExtension;
                cloudBlockBlob.UploadFromByteArray(data.FileData, 0, data.FileData.Length);
            }
            catch (Exception ex)
            {
                uploadResult.IsSuccess = false;
                uploadResult.Message = ex.ToString();

            }
            return uploadResult;
        }

        public virtual List<IUploadResult> UploadFiles(List<IFormFile> datas, string path = null)
        {
            List<IUploadResult> uploadResult = new List<IUploadResult>();
            foreach (var data in datas)
            {
                uploadResult.Add(UploadFile(data, path));
            }
            return uploadResult;
        }

        public virtual List<IUploadResult> UploadFiles(List<IByteUpload> datas, string path = null)
        {
            List<IUploadResult> uploadResult = new List<IUploadResult>();
            foreach (var data in datas)
            {
                uploadResult.Add(UploadFile(data, path));
            }
            return uploadResult;
        }
    }
}
