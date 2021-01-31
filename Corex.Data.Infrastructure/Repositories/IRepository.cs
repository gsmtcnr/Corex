using Corex.Model.Derived.EntityModel;
using Corex.Model.Infrastructure;

namespace Corex.Data.Infrastructure
{
    public interface IRepository<TEntityModel, TKey> : ITransactionDependecy
        where TEntityModel : class, IEntityModel<TKey>
    {

    }
}
