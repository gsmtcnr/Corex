using Corex.Operation.Infrastructure;
using Corex.Validation.Infrastucture;
using System.Collections.Generic;

namespace Corex.Operation.Derived.ValidationOperation
{
    public abstract class BaseValidationOperation<T> : IValidationOperation
           where T : class
    {
        public T ItemDto { get; protected set; }
        public BaseValidationOperation(T item)
        {
            ItemDto = item;
        }
        public abstract List<ValidationBase<T>> GetValidations();
        public virtual List<ValidationMessage> GetValidationResults()
        {
            List<ValidationMessage> messages = new List<ValidationMessage>();
            foreach (var validationBase in GetValidations())
            {
                if (!validationBase.IsValid)
                {
                    messages.AddRange(validationBase.Messages);
                }
            }
            return messages;
        }
    }
}
