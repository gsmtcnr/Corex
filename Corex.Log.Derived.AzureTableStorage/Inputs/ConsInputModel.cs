using Corex.Log.Infrastructure;

namespace Corex.Log.Derived.AzureTableStorage
{
    public abstract class ConsInputModel
    {
        public string TableName { get; set; }
        public IAzureLogData LogData { get; set; }
    }
}
