using Corex.Presentation.Derived.MVC.Extensions;
using Corex.Presentation.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Corex.Presentation.Derived.MVC
{
    public abstract class BaseMvcStartup : BaseStartup
    {

        protected IServiceCollection _collection;
        protected IApplicationBuilder _applicationBuilder;
        protected IWebHostEnvironment _env;
        private readonly MvcOptions _options;
        public BaseMvcStartup()
        {
            _options = SetOptions();
        }
        public abstract MvcOptions SetOptions();
        #region Configure Services
        public virtual void ConfigureServices(IServiceCollection collection)
        {
            _collection = collection;
            _collection.AddHttpContextAccessor();
            SetRazorPages();
            SetServiceSession();
            SetServiceAuthentication();

        }
        public virtual void SetSessionOptions(SessionOptions sessionOptions)
        {
            sessionOptions = new SessionOptions
            {
                Cookie = new Microsoft.AspNetCore.Http.CookieBuilder
                {
                    Name = ".AdventureWorks.Session",
                    IsEssential = true,

                },
                IdleTimeout = TimeSpan.FromHours(1)
            };
        }
        public virtual void SetRazorPages()
        {
            IMvcBuilder builder = _collection.AddRazorPages();
            IServiceProvider serviceProvider = _collection.BuildServiceProvider();
            IWebHostEnvironment env = serviceProvider.GetService<IWebHostEnvironment>();
            if (env.IsDevelopment())
                builder.AddRazorRuntimeCompilation();
            _collection.AddControllersWithViews();
        }
        #region Private Methods
        private void SetServiceAuthentication()
        {
            if (_options.UseAuthentication)
            {
                _collection.RunAuthentication();
            }
        }
        private void SetServiceSession()
        {
            if (_options.UseSession)
            {
                Action<SessionOptions> sessionAction = new Action<SessionOptions>(SetSessionOptions);
                _collection.RunSession(sessionAction);
            }
        }
        #endregion
        #endregion
        #region Configure
        public virtual void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment)
        {
            _applicationBuilder = applicationBuilder;
            _env = environment;
            if (_env.IsDevelopment())

                _applicationBuilder.UseDeveloperExceptionPage();
            else
            {
                _applicationBuilder.UseExceptionHandler(SetErrorPage());
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                _applicationBuilder.UseHsts();
            }
            _applicationBuilder.UseHttpsRedirection();
            _applicationBuilder.UseStaticFiles();
            _applicationBuilder.UseRouting();
            SetConfigureSession();
            SetConfigureAuthentication();
            SetRouting();
        }
        public virtual void SetRouting()
        {
            _applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        public virtual string SetErrorPage()
        {
            return "/Error";
        }
        #region Private Methods
        private void SetConfigureAuthentication()
        {
            if (_options.UseAuthentication)
                _applicationBuilder.RunAuthentication();
        }

        private void SetConfigureSession()
        {
            if (_options.UseSession)
                _applicationBuilder.RunSession();
        }
        #endregion
        #endregion
    }
}
