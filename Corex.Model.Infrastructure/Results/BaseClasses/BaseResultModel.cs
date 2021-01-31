using System.Collections.Generic;
using System.Linq;

namespace Corex.Model.Infrastructure
{
    public abstract class BaseResultModel : IResultModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IList<IResultMessage> Messages { get; set; }
        public virtual void SetResult()
        {
            if (Messages.Any())
            {
                IsSuccess = false;
            }
            else
                IsSuccess = true;
        }
    }
}
