using Corex.Utility.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Corex.Data.Infrastructure.Extensions
{
    public static class EntityFrameworkExtensions
    {
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
