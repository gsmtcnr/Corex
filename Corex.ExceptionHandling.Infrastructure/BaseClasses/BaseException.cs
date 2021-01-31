using Corex.ExceptionHandling.Infrastructure.Models;
using System;

namespace Corex.ExceptionHandling.Infrastructure
{
    public abstract class BaseException : Exception
    {
        public BaseException(BaseExceptionModel baseExceptionModel)
            :base(baseExceptionModel.OriginalMessage)
        {

        }
        public BaseException(BaseExceptionModel baseExceptionModel, Exception innerException)
            :base(baseExceptionModel.OriginalMessage, innerException)
        {

        }
    }
}
