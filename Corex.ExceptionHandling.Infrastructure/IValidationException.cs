using Corex.ExceptionHandling.Infrastructure.Models;

namespace Corex.ExceptionHandling.Infrastructure
{
    public interface IValidationException : IException
    {
        ValidationExceptionModel ValidationExceptionModel { get; }
    }
}
