using Corex.CloudFile.Infrastructure;

namespace Corex.PDFConverter.Infrastructure.Inputs
{
    public class PDFByteUploadInput : IByteUpload
    {
        public PDFByteUploadInput()
        {
            FileExtension = "pdf";
        }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
    }
}
