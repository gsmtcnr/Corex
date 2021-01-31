using Corex.ExceptionHandling.Infrastructure;
using Corex.ExceptionHandling.Infrastructure.Models;
using System;

namespace Corex.ExceptionHandling.Derived.Authentication
{
    public class AuthenticationOperationException : BaseException, IAuthenticationException
    {
        public AuthenticationOperationException(BaseExceptionModel baseExceptionModel) : base(baseExceptionModel)
        {
            AuthenticationOperationExceptionModel = (AuthenticationExceptionModel)baseExceptionModel;
        }

        public AuthenticationOperationException(BaseExceptionModel baseExceptionModel, Exception innerException) : base(baseExceptionModel, innerException)
        {
            AuthenticationOperationExceptionModel = (AuthenticationExceptionModel)baseExceptionModel;
        }

        public AuthenticationExceptionModel AuthenticationOperationExceptionModel { get; private set; }
    }
}
