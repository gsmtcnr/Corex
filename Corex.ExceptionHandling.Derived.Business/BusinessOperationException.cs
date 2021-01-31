using Corex.ExceptionHandling.Infrastructure;
using Corex.ExceptionHandling.Infrastructure.Models;
using System;

namespace Corex.ExceptionHandling.Derived.Business
{
    public class BusinessOperationException : BaseException, IBusinessException
    {
        public BusinessOperationException(BaseExceptionModel baseExceptionModel) : base(baseExceptionModel)
        {
            BusinessOperationExceptionModel = (BusinesOperationExceptionModel)baseExceptionModel;
        }

        public BusinessOperationException(BaseExceptionModel baseExceptionModel, Exception innerException) 
            : base(baseExceptionModel, innerException)
        {
            BusinessOperationExceptionModel = (BusinesOperationExceptionModel)baseExceptionModel;
        }

        public BusinesOperationExceptionModel BusinessOperationExceptionModel { get; private set; }
    }
}
