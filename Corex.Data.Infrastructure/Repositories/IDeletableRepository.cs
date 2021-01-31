using Corex.Model.Derived.EntityModel;

namespace Corex.Data.Infrastructure
{
    public interface IDeletableRepository<TEntityModel, TKey> :
      IRepository<TEntityModel, TKey> where TEntityModel : class, IEntityModel<TKey>
    {
        bool Delete(TEntityModel item);
    }
}
