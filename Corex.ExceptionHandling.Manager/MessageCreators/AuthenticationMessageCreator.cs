using Corex.ExceptionHandling.Manager.Models;
using Corex.ExceptionHandling.Infrastructure;
using System.Collections.Generic;

namespace Corex.ExceptionHandling.Manager.MessageCreators
{
    public class AuthenticationMessageCreator : IMessageCreator
    {
        public ExceptionMessageModel GetExceptionMessageModel(IException myException)
        {
            return AuthenticationExceptionMessage(myException as IAuthenticationException);
        }
        private ExceptionMessageModel AuthenticationExceptionMessage(IAuthenticationException authenticationException)
        {
            ExceptionMessageModel exceptionMessageModel = new ExceptionMessageModel
            {
                //OriginalMessage = authenticationException.AuthenticationOperationExceptionModel.OriginalMessage,
                Messages = new List<ExceptionMessage> {
                new ExceptionMessage{
                    Code = authenticationException.AuthenticationOperationExceptionModel.Code,
                    Message = authenticationException.AuthenticationOperationExceptionModel.GetUFMessage()
                }
            }
            };
            return exceptionMessageModel;
        }
    }
}
