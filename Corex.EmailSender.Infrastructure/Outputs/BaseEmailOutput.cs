namespace Corex.EmailSender.Infrastructure
{
    public abstract class BaseEmailOutput : IEmailOutput
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
