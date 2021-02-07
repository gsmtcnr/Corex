using Corex.PDFConverter.Infrastructure.Outputs;

namespace Corex.PDFConverter.Infrastructure
{
    public interface ICloudPDFConverter : IPDFConverter
    {
        IPDFConverterOutput HtmlToPdf(IPDFConverterInput input);
        IPDFConverterOutput UrlToPdf(IPDFConverterInput input);
    }
}
