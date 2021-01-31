using Corex.ExceptionHandling.Manager.Models;
using Corex.ExceptionHandling.Infrastructure;

namespace Corex.ExceptionHandling.Manager.MessageCreators
{
    public class BusinessMessageCreator : IMessageCreator
    {
        public ExceptionMessageModel GetExceptionMessageModel(IException myException)
        {
            return BusinessOperationExceptionMessage(myException as IBusinessException);
        }
        private ExceptionMessageModel BusinessOperationExceptionMessage(IBusinessException baseException)
        {
            ExceptionMessageModel model = new ExceptionMessageModel
            {
                Messages = new System.Collections.Generic.List<ExceptionMessage> {
                new ExceptionMessage
                {
                   Code= baseException.BusinessOperationExceptionModel.Code,
                   Message = baseException.BusinessOperationExceptionModel.GetUFMessage()
                }
            }
            };
            return model;
        }
    }
}
