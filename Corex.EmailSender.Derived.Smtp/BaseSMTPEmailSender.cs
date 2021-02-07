using Corex.EmailSender.Infrastructure;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Corex.EmailSender.Derived.SMTP
{
    public abstract class BaseSMTPEmailSender : IEmailSender
    {
        public abstract SMTPInformation CreateInformation();
        public virtual Task<IEmailOutput> SendAsync(IEmailInput emailInput)
        {
            SMTPInformation information = CreateInformation();
            SMTPOutput outputResult = new SMTPOutput();
            string to = emailInput.To;
            string subject = emailInput.Subject;
            string body = emailInput.Body;
            MailMessage mailMessage = GetMailMessage(information.From, to, subject, body);
            SmtpClient smtp = new SmtpClient(information.Host, information.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(information.From, information.Password),
                EnableSsl = true
            };
            SetResult(outputResult, mailMessage, smtp);
            return Task.FromResult<IEmailOutput>(outputResult);
        }
        #region Private Methods
        private SMTPOutput SetResult(SMTPOutput outputResult, MailMessage mailMessage, SmtpClient smtp)
        {
            try
            {
                smtp.Send(mailMessage);
                outputResult.IsSuccess = true;
            }
            catch (Exception ex)
            {
                outputResult.Message = ex.Message;
                outputResult.IsSuccess = false;
            }
            return outputResult;
        }

        private MailMessage GetMailMessage(string from, string to, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.From = new MailAddress(from);
            mailMessage.IsBodyHtml = false;
            return mailMessage;
        }
        #endregion
    }
}
