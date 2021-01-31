using Corex.Model.Derived.EntityModel;

namespace Corex.Data.Infrastructure
{
    public interface IInsertableRepository<TEntityModel, TKey> :
      IRepository<TEntityModel, TKey> where TEntityModel : class, IEntityModel<TKey>
    {
        TEntityModel Insert(TEntityModel item);
    }
}
