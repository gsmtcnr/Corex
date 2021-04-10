using Microsoft.EntityFrameworkCore;

namespace Corex.Data.Infrastructure
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
