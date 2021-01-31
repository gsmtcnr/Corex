using Corex.ExceptionHandling.Infrastructure.Models;

namespace Corex.ExceptionHandling.Infrastructure
{
    public interface IAuthenticationException : IException
    {
        AuthenticationExceptionModel AuthenticationOperationExceptionModel { get; }
    }
}
