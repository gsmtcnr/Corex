using Corex.Model.Derived.EntityModel;

namespace Corex.Data.Infrastructure
{
    public interface IUpdatableRepository<TEntityModel, TKey> :
      IRepository<TEntityModel, TKey> where TEntityModel : class, IEntityModel<TKey>
    {
        TEntityModel Update(TEntityModel item);
    }
}
