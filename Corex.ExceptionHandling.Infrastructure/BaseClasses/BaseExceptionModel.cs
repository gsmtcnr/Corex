namespace Corex.ExceptionHandling.Infrastructure.Models
{
    public abstract class BaseExceptionModel
    {
        public string OriginalMessage { get; set; }
        public string Code { get; set; }
        public string GetUFMessage()
        {
            string message = "Code: " + Code + " OriginalMessage: " + OriginalMessage;
            string uFDMessage = GetUFMessageCreate();
            if (!string.IsNullOrEmpty(uFDMessage))
                message = uFDMessage;
            return message;
        }
        public abstract string GetUFMessageCreate();

    }
}
