namespace Corex.ExceptionHandling.Infrastructure.Models
{
    public class AuthenticationExceptionModel : BaseExceptionModel
    {
        public override string GetUFMessageCreate()
        {
            //Dilersem extra bir mesaj set edip gönderelim.
            return string.Empty;
        }
    }
}
