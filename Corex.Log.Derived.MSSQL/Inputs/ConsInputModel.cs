using Corex.Log.Infrastructure;

namespace Corex.Log.Derived.MSSQL
{
    public class ConsInputModel
    {
        public string ConnectionString { get; set; }
        public ILogData LogData { get; set; }
    }
}
