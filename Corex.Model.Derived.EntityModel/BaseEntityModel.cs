using Corex.Model.Infrastructure;

namespace Corex.Model.Derived.EntityModel
{
    public abstract class BaseEntityModel<TKey> : BaseModel<TKey>, IEntityModel<TKey>
    {

    }
}
