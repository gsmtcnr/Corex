using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Text;

namespace Corex.Presentation.Derived.MVC.Extensions
{
    public static class BuilderExtensions
    {
        public static void RunAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
        public static void RunSession(this IApplicationBuilder app)
        {
            app.UseSession();
        }
    }
}
