using Corex.SmsSender.Infrastructure;

namespace Corex.SmsSender.Derived.PostaGuvercini
{
    public class PGSmsResult : ISmsOutput
    {
        public PGSmsResult(string result)
        {
            SetResult(result);
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        private void SetResult(string result)
        {
            var splitValues = result.Split('&');
            string errorNo = splitValues[0].Split('=')[1];
            string errText = splitValues[1].Split('=')[1];
            Message = result;
            if (errorNo == "0" && string.IsNullOrEmpty(errText))
                IsSuccess = true;
        }
    }
}
