namespace Corex.CloudFile.Infrastructure
{
    public interface IByteUpload
    {
        byte[] FileData { get; set; }
        string FileName { get; set; }
        string FileExtension { get; set; }
    }
}
