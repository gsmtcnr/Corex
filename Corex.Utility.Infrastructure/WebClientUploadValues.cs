using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Corex.Utility.Infrastructure
{
    public class WebClientUploadValues
    {
        private readonly string _url;
        private readonly NameValueCollection _nameValueCollection;
        private readonly string _contentType;
        public WebClientUploadValues(string url, NameValueCollection nameValueCollection, string contentType)
        {
            _url = url;
            _nameValueCollection = nameValueCollection;
            _contentType = contentType;
        }
        public string Send()
        {
            var client = new WebClient { Encoding = Encoding.UTF8 };
            client.Headers.Add("Accept:" + _contentType);
            byte[] result = client.UploadValues(_url, "POST", _nameValueCollection);
            string responseContent = Encoding.UTF8.GetString(result);
            return responseContent;
        }
    }
}
