using Corex.ExceptionHandling.Manager.Models;
using Corex.ExceptionHandling.Infrastructure;
using System.Linq;

namespace Corex.ExceptionHandling.Manager.MessageCreators
{
    public class ValidationMessageCreator : IMessageCreator
    {
        public ExceptionMessageModel GetExceptionMessageModel(IException myException)
        {
            return ValidationExceptionMessage(myException as IValidationException);
        }
        private ExceptionMessageModel ValidationExceptionMessage(IValidationException baseException)
        {
            ExceptionMessageModel model = new ExceptionMessageModel
            {
                Messages = baseException.ValidationExceptionModel.ValidationMessages.Select(v => new ExceptionMessage
                {
                    Code = v.Code,
                    Message = v.Message
                }).ToList()
            };
            return model;
        }
    }
}
