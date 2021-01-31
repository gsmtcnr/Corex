using System.Collections.Generic;

namespace Corex.Model.Infrastructure
{
    public interface IResultModel
    { 
        bool IsSuccess { get; set; }
        string Message { get; set; }
        IList<IResultMessage> Messages { get; set; }
        void SetResult();
    }
}
