namespace Corex.ExceptionHandling.Infrastructure.Models
{
    public class DatabaseOperationExceptionModel : BaseExceptionModel
    {
        public string DatabaseName { get; set; }
        public string DataSourceName { get; set; }
        public override string GetUFMessageCreate()
        {
            return string.Format("DatabaseName:{0} - DataSourceName:{1}", DatabaseName, DataSourceName);
        }
    }
}