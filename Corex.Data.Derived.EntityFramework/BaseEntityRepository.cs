using Corex.Data.Infrastructure;
using Corex.Model.Derived.EntityModel;

namespace Corex.Data.Derived.EntityFramework
{
    public abstract class BaseMSSQLEntityRepository<OC, TEntityModel, TKey> : BaseEntityRepository<OC, TEntityModel, TKey>
        where TEntityModel : class, IEntityModel<TKey>
        where OC : BaseMSSQLEntityContext, new()
    {
    }
}
