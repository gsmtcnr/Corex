using Corex.EmailSender.Infrastructure;

namespace Corex.EmailSender.Derived.SendGrid
{
    public class SendGridOutput : IEmailOutput
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
