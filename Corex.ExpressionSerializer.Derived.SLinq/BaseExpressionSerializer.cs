using Corex.ExpressionSerializer.Infrastructure;
using Serialize.Linq.Serializers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Corex.ExpressionSerializer.Derived.SLinq
{
    public class BaseExpressionSerializer : IExpressionSerializer
    {
        private readonly Serialize.Linq.Serializers.ExpressionSerializer _serializer;
        public BaseExpressionSerializer()
        {
            _serializer = new Serialize.Linq.Serializers.ExpressionSerializer(new JsonSerializer());
        }
        public string SerializeText(Expression expression)
        {

            string value = _serializer.SerializeText(expression);
            return value;
        }
        public void AddKnowType(Type type)
        {
            _serializer.AddKnownType(type);
        }
        public void AddKnowTypes(IEnumerable<Type> types)
        {
            _serializer.AddKnownTypes(types);
        }
    }
}
