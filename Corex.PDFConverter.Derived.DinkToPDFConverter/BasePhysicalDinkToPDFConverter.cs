using Corex.PDFConverter.Infrastructure;
using Corex.PDFConverter.Infrastructure.Helpers;
using Corex.PDFConverter.Infrastructure.Outputs;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Corex.PDFConverter.Derived.DinkToPDFConverter
{
    public abstract class BasePhysicalDinkToPDFConverter : FileOperationHelper, IPhysicalPDFConverter
    {
        private readonly IConverter _converter;
        public BasePhysicalDinkToPDFConverter()
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
                    GlobalSettings = {

                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Landscape,

                },

                };
                if (!string.IsNullOrEmpty(input.Source))
                    doc.Objects.Add(new ObjectSettings
                    {
                        HtmlContent = input.Source,
                        PagesCount = true,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                        FooterSettings = { FontSize = 9, Right = "Page [page] of [toPage]" }
                    });
                byte[] pdf = _converter.Convert(doc);
                string filePath = GetFilePath(input);
                FileWrite(filePath, pdf);
            }
            catch (System.Exception ex)
            {
                resultModel.Messages.Add(new PDFResultMessage
                {
                    Code = "DinkToPDF_Physical_HtmlToPdf",
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
                    GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Landscape
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
                byte[] pdf = _converter.Convert(doc);
                string filePath = GetFilePath(input);
                FileWrite(filePath, pdf);
            }
            catch (System.Exception ex)
            {
                resultModel.Messages.Add(new PDFResultMessage
                {
                    Code = "DinkToPDF_Physical_UrlToPdf",
                    Message = ex.ToString()
                });
                resultModel.IsSuccess = false;
            }
            return resultModel;
        }
    }
}
