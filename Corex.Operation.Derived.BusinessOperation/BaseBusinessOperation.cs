using Corex.ExceptionHandling.Derived.Business;
using Corex.ExceptionHandling.Infrastructure.Models;
using Corex.Operation.Inftrastructure;

namespace Corex.Operation.Derived.BusinessOperation
{
    public abstract class BaseBusinessOperation : IBusinessOperation
    {
        public virtual void NullCheck<T>(T model)
            where T : class, new()
        {
            if (model == null)
                throw new BusinessOperationException(new BusinesOperationExceptionModel
                {
                    ClassName = "BusinessOperation",
                    MethodName = "NullCheck",
                    OriginalMessage = nameof(model) + " is  null"
                });
        }
    }
}
