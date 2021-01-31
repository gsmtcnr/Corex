using System.Runtime.Serialization;

namespace Corex.Model.Infrastructure
{

    public interface IResultMessage
    {
        [DataMember]
        string Message { get; set; }
        [DataMember]
        string Code { get; set; }
    }
}
