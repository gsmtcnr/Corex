using Corex.Utility.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Corex.Data.Derived.EntityFramework
{
    public static class EntityFrameworkExtensions
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
        public static List<T> ToList<T>(this IDataReader reader)
        {
            var result = new List<T>();
            var itemType = typeof(T);
            var properties = itemType.GetPropertiesWithoutHidings();

            while (reader.Read())
            {
                T item;

                if (itemType.IsPrimitive
                    || itemType == typeof(string))
                {
                    item = TypeConvertUtility.To<T>(reader.GetValue(0));
                }
                else
                {
                    item = Activator.CreateInstance<T>();
                    foreach (var prp in properties)
                    {
                        string fieldName = prp.Name;

                        int fieldOrdinal = reader.GetFieldOrdinal(fieldName);

                        if (fieldOrdinal >= 0)
                        {
                            if (prp.PropertyType == typeof(List<int>))
                            {
                                List<int> propertyResult = null;
                                var valueAsString = TypeConvertUtility.To<string>(reader.GetValue(fieldOrdinal));
                                if (!string.IsNullOrWhiteSpace(valueAsString) && valueAsString.Contains(","))
                                {
                                    propertyResult = propertyResult = (valueAsString ?? string.Empty).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(p => int.Parse(p))
                                   .ToList();

                                }

                                prp.SetValue(item, propertyResult, null);
                            }
                            else
                            {
                                prp.SetValue(item, TypeConvertUtility.ToWithType(prp.PropertyType, reader.GetValue(fieldOrdinal)), null);
                            }
                        }
                    }
                }

                result.Add(item);
            }

            return result;
        }
        public static int GetFieldOrdinal(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

    }
}
