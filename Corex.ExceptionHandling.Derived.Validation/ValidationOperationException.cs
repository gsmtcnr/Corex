using Corex.ExceptionHandling.Infrastructure;
using Corex.ExceptionHandling.Infrastructure.Models;
using System;

namespace Corex.ExceptionHandling.Derived.Validation
{
    public class ValidationOperationException : BaseException, IValidationException
    {
        public ValidationOperationException(BaseExceptionModel baseExceptionModel) : base(baseExceptionModel)
        {
            ValidationExceptionModel = (ValidationExceptionModel)baseExceptionModel;
        }

        public ValidationOperationException(BaseExceptionModel baseExceptionModel, Exception innerException) : base(baseExceptionModel, innerException)
        {
            ValidationExceptionModel = (ValidationExceptionModel)baseExceptionModel;
        }

        public ValidationExceptionModel ValidationExceptionModel { get; private set; }

    }
}
