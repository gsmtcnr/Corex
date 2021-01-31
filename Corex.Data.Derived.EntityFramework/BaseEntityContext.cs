using Microsoft.EntityFrameworkCore;

namespace Corex.Data.Derived.EntityFramework
{
    public abstract class BaseEntityContext : DbContext
    {
        public BaseEntityContext()
        {

        }
        public BaseEntityContext(DbContextOptions options) : base(options)
        {

        }
    }
}
