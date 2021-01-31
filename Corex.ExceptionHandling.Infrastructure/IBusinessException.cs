using Corex.ExceptionHandling.Infrastructure.Models;

namespace Corex.ExceptionHandling.Infrastructure
{
    public interface IBusinessException : IException
    {
        BusinesOperationExceptionModel BusinessOperationExceptionModel { get; }
    }
}
