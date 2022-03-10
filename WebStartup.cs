using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using MVCTesterCPI2.Controllers;
using MVCTesterCPI2.Infrastructure;
using MVCTesterCPI2.Infrastructure.DbSetup;
using MVCTesterCPI2.Infrastructure.Interfaces;
using Owin;
using Polly;
using Polly.Extensions.Http;

[assembly: OwinStartup(typeof(MVCTesterCPI2.WebStartup))]

namespace MVCTesterCPI2
{
    public class WebStartup
    {
        private IConfiguration _config;
        private static readonly string cpiBaseUri = ConfigurationManager.AppSettings["CpiBaseUri"];
        EntityUpdater entityUpd;
        public void Configuration(IAppBuilder app)
        {
            Debug.WriteLine("OWIN startup");
            var sv = new ServiceCollection();
            Configure(sv);
            var serviceProvider = sv.AddHttpClient().BuildServiceProvider();
            var httpClientFact = serviceProvider.GetService<IHttpClientFactory>();
            Models.Authorization.InitiateClients(httpClientFact);
            entityUpd = new EntityUpdater(new Athenaeum(new CpiClientBase(Models.Authorization._cpiClient)));
            Athenaeum ath = new Athenaeum(new CpiClientBase(Models.Authorization._cpiClient));
            HomeController hc = new HomeController(Models.Authorization._cpiClient, entityUpd);
        }

        void Configure(IServiceCollection s)
        {
            s.AddHttpClient("cpiClient", c =>
            {
                c.BaseAddress = new Uri(cpiBaseUri);
                c.DefaultRequestHeaders.Add("Connection", "keep-alive");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddPolicyHandler(RetryPolicy()).AddPolicyHandler(FallbackPolicy());

            s.AddSingleton((implementationFactory) =>
            {
                return new Athenaeum(new CpiClientBase(Models.Authorization._cpiClient));
            });
            s.AddSingleton((implementationFactory) =>
            {
                return new HomeController(Models.Authorization._cpiClient, new EntityUpdater(new Athenaeum(new CpiClientBase(Models.Authorization._cpiClient))));
            });
            s.AddSingleton((implementationFactory) =>
            {
                return new EntityUpdater(new Athenaeum(new CpiClientBase(Models.Authorization._cpiClient)));
            });

            //s.AddScoped<IUpdater, EntityUpdater>();
            //s.AddScoped<IAthenaeum, Athenaeum>();
            //s.AddScoped<HomeController>();
            s.AddDbContextPool<LogContext>(options => options.UseSqlServer(_config.GetConnectionString("localExpressDb")));
        }

        public static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
        {
            // return HttpPolicyExtensions.HandleTransientHttpError()
            return Policy<HttpResponseMessage>.Handle<HttpRequestException>()
                .OrResult(result => statusCodesForRetry.Contains(result.StatusCode))
                .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(1) }, async (responseMessage, timeSpan, retryCount, context) =>
                {
                    if (responseMessage.Result.RequestMessage != null)
                    {
                        var requestContent = responseMessage.Result?.RequestMessage.Content != null ? await responseMessage.Result.RequestMessage.Content.ReadAsStringAsync() : "";
                        Debug.WriteLine(requestContent);
                        Debug.WriteLine($"Request unsuccessful: {responseMessage.Result.RequestMessage.RequestUri} \r\n" +
                                            $"Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
                    }
                    else
                    {
                        Debug.WriteLine($"Request unsuccessful: {responseMessage.Result.ReasonPhrase} \r\n" +
                                            $"Waiting {timeSpan} before next retry. Retry attempt {retryCount}", ConsoleColor.Blue);
                    }
                    var responseContent = await responseMessage.Result.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseContent);
                    Console.ResetColor();
                });
        }


        public static IAsyncPolicy<HttpResponseMessage> FallbackPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                    .OrResult(result => statusCodesForRetry.Contains(result.StatusCode))
                    .FallbackAsync(async b =>
                    {
                        return new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Content = new StringContent("Fallback policy used")
                        };
                    });
        }

        public static HttpStatusCode[] statusCodesForRetry =
        {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout, // 504
            HttpStatusCode.BadRequest, // 400
            HttpStatusCode.MethodNotAllowed,
            HttpStatusCode.NoContent,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.NotFound, // 404
        };
    }
}
