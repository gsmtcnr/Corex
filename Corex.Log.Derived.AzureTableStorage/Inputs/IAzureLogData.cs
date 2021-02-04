using Corex.Log.Infrastructure;
using Microsoft.WindowsAzure.Storage.Table;

namespace Corex.Log.Derived.AzureTableStorage
{
    public interface IAzureLogData : ITableEntity, ILogData
    {

    }
}
