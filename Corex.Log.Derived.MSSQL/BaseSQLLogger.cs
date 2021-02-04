using Corex.Log.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Corex.Log.Derived.MSSQL
{
    public abstract class BaseSQLLogger : ILogSync, ILogAsync
    {
        public SqlConnection Connection { get; private set; }
        public ILogData LogData { get; private set; }
        public BaseSQLLogger(ConsInputModel consInputModel)
        {
            Connection = new SqlConnection(consInputModel.ConnectionString);
            LogData = consInputModel.LogData;
        }
        /// <summary>
        /// Örn : 
        ///  ovveride SqlCommand CommandCreator()
        ///{
        /// SqlCommand cmd = new SqlCommand("sp_Log_Insert", Connection)
        ///     {
        /// CommandType = System.Data.CommandType.StoredProcedure
        ///};
        ///   cmd.Parameters.Add(new SqlParameter("CreatedDate", LogData.CreatedDate));
        ///   cmd.Parameters.Add(new SqlParameter("Log", LogData.Log));
        ///       return cmd;
        /// }
        /// </summary>
        /// <returns></returns>
        public abstract SqlCommand CreateCommand();
        public virtual void DoLog()
        {
            using SqlCommand cmd = CreateCommand();
            Connection.Open();
            cmd.ExecuteNonQuery();
            Connection.Close();
            cmd.Dispose();
            Connection.Dispose();
        }
        public virtual async Task DoLogAsync()
        {
            using SqlCommand cmd = CreateCommand();
            await Connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
