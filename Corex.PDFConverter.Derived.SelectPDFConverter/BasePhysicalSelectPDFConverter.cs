using Corex.PDFConverter.Infrastructure;
using Corex.PDFConverter.Infrastructure.Helpers;
using Corex.PDFConverter.Infrastructure.Outputs;
using SelectPdf;

namespace Corex.PDFConverter.Derived.SelectPDFConverter
{
    public abstract class BasePhysicalSelectPDFConverter : FileOperationHelper, IPhysicalPDFConverter
    {
        private readonly HtmlToPdf _converter;
        public BasePhysicalSelectPDFConverter()
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
                FileWriteRead(filePath);
                doc.Save(filePath);
                doc.Close();
            }
            catch (System.Exception ex)
            {
                resultModel.Messages.Add(new PDFResultMessage
                {
                    Code = "Physical_HtmlToPdf",
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
                FileWriteRead(filePath);
                doc.Save(filePath);
                doc.Close();
            }
            catch (System.Exception ex)
            {
                resultModel.Messages.Add(new PDFResultMessage
                {
                    Code = "Physical_UrlToPdf",
                    Message = ex.ToString()
                });
                resultModel.IsSuccess = false;
            }
            return resultModel;
        }
    }
}
