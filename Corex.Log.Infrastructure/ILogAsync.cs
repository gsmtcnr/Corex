using System.Threading.Tasks;

namespace Corex.Log.Infrastructure
{
    public interface ILogAsync : ILog
    {
        Task DoLogAsync();
    }
}
