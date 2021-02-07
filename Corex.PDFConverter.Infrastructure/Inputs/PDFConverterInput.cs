namespace Corex.PDFConverter.Infrastructure
{
    public class PDFConverterInput : IPDFConverterInput
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public string Path { get; set; }
    }
}
