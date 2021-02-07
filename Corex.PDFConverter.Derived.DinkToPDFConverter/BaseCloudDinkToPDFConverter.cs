using Corex.CloudFile.Infrastructure;
using Corex.PDFConverter.Infrastructure;
using Corex.PDFConverter.Infrastructure.Inputs;
using Corex.PDFConverter.Infrastructure.Outputs;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Corex.PDFConverter.Derived.DinkToPDFConverter
{
    public abstract class BaseCloudDinkToPDFConverter : ICloudPDFConverter
    {
        public abstract IUploadAsync GetUploadAsync();
        private readonly IConverter _converter;
        public BaseCloudDinkToPDFConverter()
        {
            _converter = new SynchronizedConverter(new Corex.PDFConverter.Derived.DinkToPDFConverter.PdfTools());
        }
        public IPDFConverterOutput HtmlToPdf(IPDFConverterInput input)
        {
            IPDFConverterOutput resultModel = new PDFConverterOutput
            {
                IsSuccess = true
            };
            try
            {
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings =
                    {
                     PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                    }
                };

                if (!string.IsNullOrEmpty(input.Source))
                    doc.Objects.Add(new ObjectSettings
                    {
                        HtmlContent = input.Source,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                        FooterSettings = { FontSize = 9, Right = "Page [page] of [toPage]" }
                    });

                byte[] fileData = _converter.Convert(doc);
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
                    Code = "DinkToPDFConverter_Cloud_HtmlToPdf",
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
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings =
                    {

                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,

                    }

                };

                if (!string.IsNullOrEmpty(input.Source))
                    doc.Objects.Add(new ObjectSettings
                    {
                        Page = input.Source,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                        FooterSettings = { FontSize = 9, Right = "Page [page] of [toPage]" }
                    });

                byte[] fileData = _converter.Convert(doc);
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
                    Code = "DinkToPDFConverter_Cloud_UrlToPdf",
                    Message = ex.ToString()
                });
                resultModel.IsSuccess = false;
            }
            return resultModel;
        }
    }
}
