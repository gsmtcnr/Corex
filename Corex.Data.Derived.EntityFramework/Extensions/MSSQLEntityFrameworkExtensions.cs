using Corex.Data.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Corex.Data.Derived.EntityFramework
{
    public static class MSSQLEntityFrameworkExtensions
    {
        public static List<T> ExecuteProcedure<T>(this DbContext context, string commandText, Dictionary<string, object> parameters, 
            CommandType commandType = CommandType.StoredProcedure)
        {
            List<T> result = null;

            context.ExecuteReader(commandText, parameters, (reader) => result = reader.ToList<T>(), commandType: commandType);

            return result;
        }
        public static void ExecuteReader(this DbContext context, string commandText, Dictionary<string, object> parameters,
            Action<IDataReader> use, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = commandType;
                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        SqlParameter sqlParameter = null;
                        if (item.Value == null)
                            sqlParameter = new SqlParameter(item.Key, DBNull.Value);
                        else if (item.Value.GetType() == typeof(SqlParameter))
                            sqlParameter = (SqlParameter)item.Value;
                        else
                            sqlParameter = new SqlParameter(item.Key, item.Value);
                        command.Parameters.Add(sqlParameter);
                    }
                }
                context.Database.OpenConnection();
                use(command.ExecuteReader());
                context.Database.CloseConnection();
            }
        }

    }
}
