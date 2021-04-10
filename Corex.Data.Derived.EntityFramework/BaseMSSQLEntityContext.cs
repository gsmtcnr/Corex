using Corex.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Corex.Data.Derived.EntityFramework
{
    public abstract class BaseMSSQLEntityContext : BaseEntityContext
    {
        protected BaseMSSQLEntityContext()
        {
        }

        protected BaseMSSQLEntityContext(DbContextOptions options) : base(options)
        {
        }
    }
}
