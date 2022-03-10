using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MVCTesterCPI2.Controllers;
using MVCTesterCPI2.Infrastructure;
using MVCTesterCPI2.Infrastructure.Interfaces;
using MVCTesterCPI2.Models;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Services.Description;

namespace MVCTesterCPI2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly string cpiBaseUri = ConfigurationManager.AppSettings["CpiBaseUri"];

        private IServiceProvider ServiceProvider;
        private string globalToken;
        public MvcApplication()
        {
            var wrapper = new EventHandlerTaskAsyncHelper(DoAsync);
            this.AddOnAuthenticateRequestAsync(wrapper.BeginEventHandler, wrapper.EndEventHandler);
        }

        public async Task DoAsync(object sender, EventArgs e)
        {
            await Models.Authorization.FillTokens();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Microsoft Extensions Hosting:
            // var hostBuilder = new HostBuilder();
            // hostBuilder.ConfigureServices(ConfigureServices);
            // var host = hostBuilder.Build();
            // ServiceProvider = host.Services;
        }

        protected void Application_BeginRequest()
        {
           RecordEvent("BeginRequest()");
        }
        protected void Application_AuthenticateRequest()
        {
           RecordEvent("AuthenticateRequest()");
        }
        protected void Application_PostAuthenticateRequest()
        {
           RecordEvent("PostAuthenticateRequest()");
        }

        protected void RecordEvent(string name)
        {
           List<string> eventList = Application["events"] as List<string>;
           if (eventList == null)
           {
               Application["events"] = eventList = new List<string>();
           }
           eventList.Add(name);
        }
    }
}
