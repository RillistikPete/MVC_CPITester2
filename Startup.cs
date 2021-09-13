using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCTesterCPI2.Infrastructure;
using MVCTesterCPI2.Infrastructure.Interfaces;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;

[assembly: FunctionsStartup(typeof(MVCTesterCPI2.Startup))]

namespace MVCTesterCPI2
{
    public class Startup
    {
        private static readonly string cpiBaseUri = ConfigurationManager.AppSettings["CpiBaseUri"];
        private IConfiguration _config;

        public void ConfigureServices(IServiceCollection services)
        {

            // services.AddTransient<ExecutingHandler>();
            services.AddHttpClient<ICpiClient, CpiClientBase>("cpiClient", c =>
            {
                c.BaseAddress = new Uri(cpiBaseUri);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddPolicyHandler(RetryPolicy()).AddPolicyHandler(FallbackPolicy());
            //services.AddMvc();
            services.AddScoped<IAthenaeum, Athenaeum>();
            services.AddScoped<ICpiClient, CpiClientBase>();
            // services.AddDbContextPool<LogContext>(options => options.UseSqlServer(_config.GetConnectionString("LocalExpressDb")));
        }

        private IAsyncPolicy<HttpResponseMessage> RetryPolicy()
        {
            // return HttpPolicyExtensions.HandleTransientHttpError()
            return Policy<HttpResponseMessage>.Handle<HttpRequestException>()
                .OrResult(result => statusCodesForRetry.Contains(result.StatusCode))
                .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5) }, async (responseMessage, timeSpan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    if (responseMessage.Result.RequestMessage != null)
                    {
                        var requestContent = responseMessage.Result?.RequestMessage.Content != null ? await responseMessage.Result.RequestMessage.Content.ReadAsStringAsync() : "";
                        Console.WriteLine(requestContent);
                        Console.WriteLine($"Request unsuccessful: {responseMessage.Result.RequestMessage.RequestUri} \r\n" +
                                            $"Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
                    }
                    else
                    {
                        Console.WriteLine($"Request unsuccessful: {responseMessage.Result.ReasonPhrase} \r\n" +
                                            $"Waiting {timeSpan} before next retry. Retry attempt {retryCount}", ConsoleColor.Blue);
                    }
                    var responseContent = await responseMessage.Result.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    Console.ResetColor();
                });
        }


        static IAsyncPolicy<HttpResponseMessage> FallbackPolicy()
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

        private static HttpStatusCode[] statusCodesForRetry =
        {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout, // 504
            HttpStatusCode.BadRequest,
            HttpStatusCode.MethodNotAllowed,
            HttpStatusCode.NoContent,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.NotFound,
        };
    }
}