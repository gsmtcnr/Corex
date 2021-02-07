namespace Corex.EmailSender.Infrastructure
{
    public abstract class BaseEmailInput : IEmailInput
    {
        public string To { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
    }
}
