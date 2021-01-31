using Corex.ExceptionHandling.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corex.ExceptionHandling.Infrastructure
{
    public interface IDataException : IException
    {
        DatabaseOperationExceptionModel DataOperationExceptionModel { get; }
    }
}
