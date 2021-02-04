namespace Corex.Log.Infrastructure
{
    public interface ILogSync : ILog
    {
        void DoLog();
    }
}
