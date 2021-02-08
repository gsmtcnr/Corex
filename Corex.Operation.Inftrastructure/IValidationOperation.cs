using Corex.Model.Infrastructure;
using Corex.Validation.Infrastucture;
using System.Collections.Generic;

namespace Corex.Operation.Infrastructure
{
    public interface IValidationOperation : ISingletonDependecy
    {
        List<ValidationMessage> GetValidationResults();
    }
}
