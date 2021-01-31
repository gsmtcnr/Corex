

using System.Collections.Generic;

namespace Corex.ExceptionHandling.Infrastructure.Models
{
    public class ValidationExceptionModel : BaseExceptionModel
    {
        public ValidationExceptionModel()
        {
            ValidationMessages = new List<ValidationExceptionMessage>();
        }
        public string ModelName { get; set; }
        public List<ValidationExceptionMessage> ValidationMessages { get; set; }

        public override string GetUFMessageCreate()
        {
            return string.Format("ModelName:{0} - ValidationMessages:{1}", ModelName, string.Join(",", ValidationMessages));
        }
    }
    public class ValidationExceptionMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}