
using Corex.Log.Infrastructure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace Corex.Log.Derived.AzureTableStorage
{
    public abstract class BaseTableStorageLogger : ILogSync, ILogAsync
    {
        public CloudStorageAccount LogStorageAccount { get; private set; }
        public IAzureLogData LogData { get; private set; }
        private readonly CloudTable _table;
        public BaseTableStorageLogger(ConsConnectionStringInputModel consInputModel)
        {
            LogStorageAccount = CloudStorageAccount.Parse(consInputModel.ConnectionString);
            LogData = consInputModel.LogData;
            _table = SetTable(consInputModel.TableName);
        }
        public BaseTableStorageLogger(ConsAccountInputModel consAccountInputModel)
        {
            LogStorageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(consAccountInputModel.AccountName, consAccountInputModel.AccountPassword), true);
            LogData = consAccountInputModel.LogData;
            _table = SetTable(consAccountInputModel.TableName);
        }
        private CloudTable SetTable(string tableName)
        {
            //Client  
            CloudTableClient tableClient = LogStorageAccount.CreateCloudTableClient();
            //Table  
            CloudTable table = tableClient.GetTableReference(tableName);
            return table;
        }
        public void DoLog()
        {
            DoLogAsync().GetAwaiter().GetResult();
        }

        public async Task DoLogAsync()
        {
            TableOperation insertOperation = TableOperation.Insert(LogData);
            await _table.ExecuteAsync(insertOperation);
        }
    }
}
