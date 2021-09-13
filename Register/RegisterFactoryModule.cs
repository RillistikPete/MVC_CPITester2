using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCTesterCPI2.Infrastructure;
using MVCTesterCPI2.Infrastructure.Interfaces;
using MVCTesterCPI2.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MVCTesterCPI2.Register
{
    public class RegisterFactoryModule : NinjectModule
    {
        private static readonly string cpiBaseUri = ConfigurationManager.AppSettings["CpiBaseUri"];
        public override void Load()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureServices(ConfigureServices);
            Bind<IHttpClientFactory>().ToConstant(hostBuilder.Build().Services.GetService<IHttpClientFactory>());
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // services.AddTransient<ExecutingHandler>();
            services.AddHttpClient<ICpiClient, CpiClientBase>("cpiClient", c =>
            {
                c.BaseAddress = new Uri(cpiBaseUri);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            //services.AddMvc();
            services.AddScoped<IAthenaeum, Athenaeum>();
            services.AddScoped<ICpiClient, CpiClientBase>();
            // services.AddDbContextPool<LogContext>(options => options.UseSqlServer(_config.GetConnectionString("LocalExpressDb")));
        }
    }
}