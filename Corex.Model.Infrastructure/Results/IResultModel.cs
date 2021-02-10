using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Corex.Model.Infrastructure
{
    public interface IResultModel
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
        List<MessageItem> Messages { get; set; }
        void SetResult();
    }
    public class MessageItem
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Code { get; set; }
    }
}
