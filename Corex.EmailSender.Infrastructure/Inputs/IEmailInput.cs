namespace Corex.EmailSender.Infrastructure
{
    public interface IEmailInput
    {
        string To { get; set; }
        string Bcc { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        string FromEmail { get; set; }
        string FromName { get; set; }
    }
}
