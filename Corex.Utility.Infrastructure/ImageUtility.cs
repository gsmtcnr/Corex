using System.IO;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

using System.Threading.Tasks;

namespace Corex.Utility.Infrastructure
{
    public class ImageUtility
    {
        public async Task<Image> DownloadImage(string url)
        {
            Image image = null;
            using (HttpClient httpClient = new HttpClient())

            using (HttpResponseMessage response = await httpClient.GetAsync(url))

            using (Stream inputStream = await response.Content.ReadAsStreamAsync())

            using (Bitmap temp = new Bitmap(inputStream))

                image = new Bitmap(temp);

            return image;
        }
        public Image CropImage(Image image, int width, int height)
        {

            var resized = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(resized))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(image, 0, 0, width, height);

            }
            return resized;
        }

    }
}
