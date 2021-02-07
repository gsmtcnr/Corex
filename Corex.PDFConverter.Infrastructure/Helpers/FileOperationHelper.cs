using System.IO;

namespace Corex.PDFConverter.Infrastructure.Helpers
{
    public class FileOperationHelper
    {
        public string FileRead(string fileName)
        {
            return File.ReadAllText(fileName);
        }
        public byte[] FileWriteRead(string fileName)
        {
            byte[] buff = null;
            if (!File.Exists(fileName))
            {
                using (FileStream fs = File.Create(fileName))
                {
                    BinaryReader br = new BinaryReader(fs);
                    long numBytes = new FileInfo(fileName).Length;
                    buff = br.ReadBytes((int)numBytes);
                    return buff;
                }
            }
            else
            {
                using (FileStream fs = File.OpenRead(fileName))
                {
                    BinaryReader br = new BinaryReader(fs);
                    long numBytes = new FileInfo(fileName).Length;
                    buff = br.ReadBytes((int)numBytes);
                    return buff;
                }
            }
        }
        public void FileWrite(string fileName, byte[] pdf)
        {
            using (FileStream fs = File.OpenWrite(fileName))
            {
                fs.Write(pdf, 0, pdf.Length);
            }
        }
        public byte[] FileSystemToBinaryByte(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public void FileDelete(string fileName)
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            File.Delete(fileName);
        }
        public string GetFilePath(IPDFConverterInput pDFConverterInput)
        {
            string pathFormat = "{0}/{1}";
            return string.Format(pathFormat, pDFConverterInput.Path, pDFConverterInput.Name);
        }
    }
}
