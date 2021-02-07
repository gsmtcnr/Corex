using Corex.Model.Infrastructure;
using System.Threading.Tasks;

namespace Corex.Push.Infrastructure
{
    public interface IPushSender : ISingletonDependecy
    {
        IPushOutput Send(IPushInput input);
        Task<IPushOutput> SendAsync(IPushInput input);
    }
}
