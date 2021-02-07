namespace Corex.PDFConverter.Infrastructure
{
    public interface IPDFConverterInput
    {
        string Name { get; set; }
        string Source { get; set; }
        string Path { get; set; }
    }
}
