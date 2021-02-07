using Corex.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Corex.ExpressionSerializer.Infrastructure
{
    public interface IExpressionSerializer : ISingletonDependecy
    {
        string SerializeText(Expression expression);
        void AddKnowType(Type type);
        void AddKnowTypes(IEnumerable<Type> types);
    }
}
