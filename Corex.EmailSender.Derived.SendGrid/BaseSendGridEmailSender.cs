using Corex.EmailSender.Infrastructure;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Corex.EmailSender.Derived.SendGrid
{
    public abstract class BaseSendGridEmailSender : IEmailSender
    {
        public abstract SendGridInformation CreateInformation();
        public virtual async Task<IEmailOutput> SendAsync(IEmailInput emailInput)
        {
            SendGridInformation sendGridInformation = CreateInformation();
            SendGridClient client = new SendGridClient(sendGridInformation.ApiKey);
            SendGridMessage msg = new SendGridMessage()
            {
                From = new EmailAddress(emailInput.FromEmail, name: emailInput.FromName),
                Subject = emailInput.Subject,
                HtmlContent = emailInput.Body
            };
            msg.AddTo(new EmailAddress(emailInput.To));
            if (!string.IsNullOrEmpty(emailInput.Bcc))
                msg.AddBcc(new EmailAddress(emailInput.Bcc));

            Response result = await client.SendEmailAsync(msg);
            SendGridOutput sendGridOutput = new SendGridOutput
            {
                IsSuccess = result.StatusCode == System.Net.HttpStatusCode.Accepted
            };
            return sendGridOutput;
        }
    }
}
