using Corex.Model.Infrastructure;

namespace Corex.Model.Derived.DtoModel
{
    public abstract class BaseDtoModel<TKey> : BaseModel<TKey>, IDtoModel<TKey>
    {

    }
}
