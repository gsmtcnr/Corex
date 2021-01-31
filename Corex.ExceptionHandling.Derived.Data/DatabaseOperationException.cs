using Corex.ExceptionHandling.Infrastructure;
using Corex.ExceptionHandling.Infrastructure.Models;
using System;

namespace Corex.ExceptionHandling.Derived.Data
{
    public class DatabaseOperationException : BaseException, IDataException
    {
        public DatabaseOperationException(BaseExceptionModel baseExceptionModel) : base(baseExceptionModel)
        {
            DataOperationExceptionModel = (DatabaseOperationExceptionModel)baseExceptionModel;
        }

        public DatabaseOperationException(BaseExceptionModel baseExceptionModel, Exception innerException) : base(baseExceptionModel, innerException)
        {
            DataOperationExceptionModel = (DatabaseOperationExceptionModel)baseExceptionModel;           
        }

        public DatabaseOperationExceptionModel DataOperationExceptionModel { get; private set; }
    }
}
