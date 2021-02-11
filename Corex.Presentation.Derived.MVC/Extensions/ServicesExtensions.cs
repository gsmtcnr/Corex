using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Corex.Presentation.Derived.MVC.Extensions
{
    public static class ServicesExtensions
    {
        public static void RunAuthentication(this IServiceCollection collection)
        {
            //collection.AddAuthenticationCore();
            collection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        }
        public static void RunSession(this IServiceCollection collection, Action<SessionOptions> sessionOptions)
        {
            collection.AddDistributedMemoryCache();
            collection.AddSession(sessionOptions);
        }
    }
}
