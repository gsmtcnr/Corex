using Corex.ExceptionHandling.Manager.Models;
using Corex.ExceptionHandling.Infrastructure;

namespace Corex.ExceptionHandling.Manager.MessageCreators
{
    public class DataMessageCreator : IMessageCreator
    {
        public ExceptionMessageModel GetExceptionMessageModel(IException myException)
        {
            return DataExceptionMesage(myException as IDataException);
        }
        private ExceptionMessageModel DataExceptionMesage(IDataException baseException)
        {
            ExceptionMessageModel model = new ExceptionMessageModel
            {
                Messages = new System.Collections.Generic.List<ExceptionMessage> {
                    new ExceptionMessage
                    {
                        Code = baseException.DataOperationExceptionModel.Code,
                        Message = baseException.DataOperationExceptionModel.GetUFMessage()
                    }
                }
            };
            return model;
        }
    }
}
