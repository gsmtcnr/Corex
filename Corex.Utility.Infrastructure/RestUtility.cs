using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Corex.Utility.Infrastructure
{
    public class RestUtility<T> where T : class
    {
        private HttpWebRequest _webRequest;
        private readonly string _url;
        private readonly string _contentType;
        private readonly string _method;
        private Encoding _encodingCode;
        private readonly Dictionary<string, string> _headers;
        public RestUtility(string url, string method = "GET", string contentType = "application/json", Dictionary<string, string> headers = null)
        {

            _url = url;
            _contentType = contentType;
            _method = method;
            _encodingCode = Encoding.UTF8;
            _headers = headers;
            CreateWebRequest();
        }
        public void SetEncoding(Encoding encoding)
        {
            _encodingCode = encoding;
        }
        #region Private  Methods
        private void CreateWebRequest()
        {
            _webRequest = (HttpWebRequest)WebRequest.Create(_url);
            _webRequest.Method = _method;
            _webRequest.Accept = _contentType;
            _webRequest.ContentType = _contentType;
            //_webRequest.Headers.Add("Content-Encoding", "utf-8");
            if (_headers != null)
            {
                foreach (var item in _headers)
                {
                    _webRequest.Headers.Add(item.Key, item.Value);
                }
            }
        }
        private void SetBody(object requestBodyObject)
        {
            if (requestBodyObject != null)
            {
                string requestBody = string.Empty;
                if (requestBodyObject is string)
                    requestBody = requestBodyObject.ToString();
                else
                    requestBody = JsonSerializer.Serialize(requestBodyObject);

                UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] byteArray = encoding.GetBytes(requestBody);
                _webRequest.ContentLength = byteArray.Length;
                using Stream dataStream = _webRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
        }
        private void SetAuthorization(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                _webRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(userName + ":" + password));
            }
        }
        private void SetAuthorization(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _webRequest.Headers["Authorization"] = "Bearer " + token;
            }
        }
        #endregion
        #region CallAsync
        public async Task<T> CallAsync()
        {
            return await CallAsync(null, string.Empty, string.Empty, string.Empty);
        }
        public async Task<T> CallAsync(object requestBodyObject)
        {
            return await CallAsync(requestBodyObject, string.Empty, string.Empty, string.Empty);
        }
        public async Task<T> CallAsync(string token)
        {
            return await CallAsync(null, string.Empty, string.Empty, token);
        }
        public async Task<T> CallAsync(object requestBodyObject, string token)
        {
            return await CallAsync(requestBodyObject, string.Empty, string.Empty, token);
        }
        public async Task<T> CallAsync(string userName, string password)
        {
            return await CallAsync(null, userName, password, string.Empty);
        }
        public async Task<T> CallAsync(object requestBodyObject, string userName, string password, string token)
        {
            SetBody(requestBodyObject);
            SetAuthorization(userName, password);
            SetAuthorization(token);
            return await GetAsyncResult();
        }
        private async Task<T> GetAsyncResult()
        {
            WebResponse response = await _webRequest.GetResponseAsync();
            if (response == null)
            {
                return null;
            }
            StreamReader streamReader = new StreamReader(response.GetResponseStream(), _encodingCode);
            string responseContent = streamReader.ReadToEnd().Trim();
            T jsonObject = JsonSerializer.Deserialize<T>(responseContent);
            return jsonObject;
        }
        #endregion
        #region Call
        public T Call()
        {
            return Call(null, string.Empty, string.Empty, string.Empty);
        }
        public T Call(object requestBodyObject)
        {
            return Call(requestBodyObject, string.Empty, string.Empty, string.Empty);
        }
        public T Call(string token)
        {
            return Call(null, string.Empty, string.Empty, token);
        }
        public T Call(object requestBodyObject, string token)
        {
            return Call(requestBodyObject, string.Empty, string.Empty, token);
        }
        public T Call(string userName, string password)
        {
            return Call(null, userName, password, string.Empty);
        }
        public T Call(object requestBodyObject, string userName, string password, string token)
        {
            SetBody(requestBodyObject);
            SetAuthorization(userName, password);
            SetAuthorization(token);
            return GetResult();
        }

        private T GetResult()
        {
            WebResponse response = _webRequest.GetResponse();
            if (response == null)
            {
                return null;
            }
            StreamReader streamReader = new StreamReader(response.GetResponseStream(), _encodingCode);
            string responseContent = streamReader.ReadToEnd().Trim();
            T jsonObject = JsonSerializer.Deserialize<T>(responseContent);
            return jsonObject;
        }
        #endregion
    }
}
