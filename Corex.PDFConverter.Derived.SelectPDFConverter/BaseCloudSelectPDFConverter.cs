using Corex.CloudFile.Infrastructure;
using Corex.PDFConverter.Infrastructure;
using Corex.PDFConverter.Infrastructure.Helpers;
using Corex.PDFConverter.Infrastructure.Inputs;
using Corex.PDFConverter.Infrastructure.Outputs;
using SelectPdf;

namespace Corex.PDFConverter.Derived.SelectPDFConverter
{
    public abstract class BaseCloudSelectPDFConverter : FileOperationHelper, ICloudPDFConverter
    {
        private readonly HtmlToPdf _converter;
        public abstract IUploadAsync GetUploadAsync();
        public BaseCloudSelectPDFConverter()
        {
            _converter = new HtmlToPdf();

        }  
        public IPDFConverterOutput HtmlToPdf(IPDFConverterInput input)
        {
            IPDFConverterOutput resultModel = new PDFConverterOutput
            {
                IsSuccess = true
            };
            try
            {
                PdfDocument doc = _converter.ConvertHtmlString(input.Source);
                string filePath = GetFilePath(input);
                doc.Save(filePath);//temp olarak kayıt et..
                doc.Close();
                byte[] fileData = FileWriteRead(filePath);     
             
                //byte[] fileData = File.ReadAllBytes(filePath);
                FileDelete(filePath);
                var cloudAsyncUpload = GetUploadAsync();
                cloudAsyncUpload.UploadAsyncFile(new PDFByteUploadInput
                {
                    FileData = fileData,
                    FileName = input.Name
                }, path: input.Path);

            }
            catch (System.Exception ex)
            {
                resultModel.Messages.Add(new PDFResultMessage
                {
                    Code = "Cloud_HtmlToPdf",
                    Message = ex.ToString()
                });
                resultModel.IsSuccess = false;
            }
            return resultModel;
        }

        public IPDFConverterOutput UrlToPdf(IPDFConverterInput input)
        {
            IPDFConverterOutput resultModel = new PDFConverterOutput
            {
                IsSuccess = true
            };
            try
            {
                PdfDocument doc = _converter.ConvertUrl(input.Source);
                string filePath = GetFilePath(input);
                doc.Save(filePath);//temp olarak kayıt et..
                doc.Close();
                byte[] fileData = FileWriteRead(filePath);

                //byte[] fileData = File.ReadAllBytes(filePath);
                FileDelete(filePath);
                var cloudAsyncUpload = GetUploadAsync();
                cloudAsyncUpload.UploadAsyncFile(new PDFByteUploadInput
                {
                    FileData = fileData,
                    FileName = input.Name
                }, path: input.Path);

            }
            catch (System.Exception ex)
            {
                resultModel.Messages.Add(new PDFResultMessage
                {
                    Code = "Cloud_HtmlToPdf",
                    Message = ex.ToString()
                });
                resultModel.IsSuccess = false;
            }
            return resultModel;
        }
    }
}
