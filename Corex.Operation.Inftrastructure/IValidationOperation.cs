using Corex.Model.Infrastructure;
using Corex.Validation.Infrastucture;
using System.Collections.Generic;

namespace Corex.Operation.Infrastructure
{
    public interface IValidationOperation<T> : ISingletonDependecy
    where T : class
    {
        public void SetItem(T item);
        List<ValidationBase<T>> GetValidations();
        List<ValidationMessage> GetValidationResults();
    }
}
