using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Corex.Utility.Infrastructure
{
    public static class LinqUtility
    {
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static void AddRange<T, S>(this Dictionary<T, S> source,
             Dictionary<T, S> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("Empty collection");
            }

            foreach (var item in collection)
            {
                if (!source.ContainsKey(item.Key))
                {
                    source.Add(item.Key, item.Value);
                }
            }
        }
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            // need to detect whether they use the same
            // parameter instance; if not, they need fixing
            ParameterExpression param = expr1.Parameters[0];
            if (ReferenceEquals(param, expr2.Parameters[0]))
            {
                // simple version
                return Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(expr1.Body, expr2.Body), param);
            }
            // otherwise, keep expr1 "as is" and invoke expr2
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    expr1.Body,
                    Expression.Invoke(expr2, param)), param);
        }

        public static IEnumerable<TItem> Extract<TList, TItem>(this IEnumerable<TList> obj
         , Func<TList, IEnumerable<TItem>> field)
        {
            foreach (var item in obj)
            {
                foreach (var i in field(item))
                {
                    yield return i;
                }
            }
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        public static IQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        public static IQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> obj, TKey key, TValue value)
        {
            if (obj.ContainsKey(key))
            {
                obj[key] = value;
            }
            else
            {
                obj.Add(key, value);
            }
        }
        static IQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            bool isSorted = false;

            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                if (pi == null)
                {
                    continue;
                }
                isSorted = true;
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            if (isSorted)
            {
                Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
                LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

                object result = typeof(Queryable).GetMethods().Single(
                        method => method.Name == methodName
                                && method.IsGenericMethodDefinition
                                && method.GetGenericArguments().Length == 2
                                && method.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), type)
                        .Invoke(null, new object[] { source, lambda });
                return (IOrderedQueryable<T>)result;
            }

            return source;
        }

        public static void Sort<TEntity, TKey>(this List<TEntity> entityList, Func<TEntity, TKey> keySelector)
        {
            Sort(entityList, keySelector, 0, entityList.Count - 1);
        }
        private static void Sort<TEntity, TKey>(this List<TEntity> entityList, Func<TEntity, TKey> keySelector, int startIndex, int endIndex)
        {
            int middleIndex;

            if (endIndex > startIndex)
            {
                middleIndex = (endIndex + startIndex) / 2;

                Sort(entityList, keySelector, startIndex, middleIndex);
                Sort(entityList, keySelector, (middleIndex + 1), endIndex);
                Merge(entityList, keySelector, startIndex, (middleIndex + 1), endIndex);
            }
        }
        private static void Merge<TEntity, TKey>(List<TEntity> entityList, Func<TEntity, TKey> keySelector, int startIndex, int middleIndex, int endIndex)
        {
            var tempList = Enumerable.Repeat<TEntity>(default(TEntity), (middleIndex + endIndex)).ToList();

            int eol, num, pos;

            eol = (middleIndex - 1);
            pos = startIndex;
            num = (endIndex - startIndex + 1);

            while ((startIndex <= eol) && (middleIndex <= endIndex))
            {
                var compareResult = Comparer<TKey>.Default.Compare(entityList.Select(keySelector).ElementAt(startIndex), entityList.Select(keySelector).ElementAt(middleIndex));

                if (compareResult < 0)
                    tempList[pos++] = entityList.ElementAt(startIndex++);
                else
                    tempList[pos++] = entityList.ElementAt(middleIndex++);
            }

            while (startIndex <= eol)
            {
                tempList[pos++] = entityList.ElementAt(startIndex++);
            }

            while (middleIndex <= endIndex)
            {
                tempList[pos++] = entityList.ElementAt(middleIndex++);
            }

            for (int i = 0; i < num; i++)
            {
                entityList[endIndex] = tempList[endIndex];
                endIndex--;
            }
        }
    }
}
