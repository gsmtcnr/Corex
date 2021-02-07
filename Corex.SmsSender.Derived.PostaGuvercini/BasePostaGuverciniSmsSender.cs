using Corex.SmsSender.Infrastructure;
using Corex.Utility.Infrastructure;
using System.Collections.Specialized;

namespace Corex.SmsSender.Derived.PostaGuvercini
{
    public abstract class BasePostaGuverciniSmsSender : ISmsSender
    {
        private string _userName;
        private string _password;
        private string _url;
        public abstract string SetUserName();
        public abstract string SetPassword();
        public abstract string SetUrl();
        private void Creator()
        {
            _userName = SetUserName();
            _password = SetPassword();
            _url = SetUrl();
        }
        public virtual ISmsOutput Send(ISmsInput sms)
        {
            Creator();
            sms.Phone = sms.Phone.ToPhoneFormat();
            NameValueCollection nameValueCollection = new NameValueCollection
                     {
                         { "user", _userName },
                         { "password", _password },
                         { "gsm",  sms.Phone },
                         { "text", sms.Text }
                     };
            WebClientUploadValues webClientUploadValues = new WebClientUploadValues(_url, nameValueCollection, "application/x-www-form-urlencoded");
            string result = webClientUploadValues.Send();
            ISmsOutput smsResult = new PGSmsResult(result);
            return smsResult;
        }
    }
}
