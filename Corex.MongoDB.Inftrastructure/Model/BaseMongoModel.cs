using Corex.Model.Infrastructure;
using System;

namespace Corex.MongoDB.Inftrastructure
{
    public abstract class BaseMongoModel : BaseModel<Guid>, IModel<Guid>
    {
    }
}
