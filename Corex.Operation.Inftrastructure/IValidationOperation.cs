using Corex.Model.Infrastructure;
using Corex.Validation.Infrastucture;
using System.Collections.Generic;

namespace Corex.Operation.Infrastructure
{
    public interface IValidationOperation<T> : ISingletonDependecy
    where T : class
    {
        T Item { get;  set; }
        List<ValidationBase<T>> GetValidations();
        List<ValidationMessage> GetValidationResults();
    }
}
