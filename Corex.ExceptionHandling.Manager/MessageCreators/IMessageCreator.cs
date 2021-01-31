using Corex.ExceptionHandling.Manager.Models;
using Corex.ExceptionHandling.Infrastructure;

namespace Corex.ExceptionHandling.Manager.MessageCreators
{
    public interface IMessageCreator
    {
        ExceptionMessageModel GetExceptionMessageModel(IException myException);
    }
}
