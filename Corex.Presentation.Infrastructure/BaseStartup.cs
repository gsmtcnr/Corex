using Microsoft.Extensions.Configuration;
using System;

namespace Corex.Presentation.Infrastructure
{
    public abstract class BaseStartup
    {
        public IConfigurationRoot Configuration { get; }
        public BaseStartup()
        {
            IConfigurationBuilder builder = SetBuilder();
            this.Configuration = builder.Build();
        }
        public virtual IConfigurationBuilder SetBuilder()
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true);
            return builder;
        }
    }
}
