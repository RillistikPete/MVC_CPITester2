using MVCTesterCPI2.Infrastructure.DbSetup;
using MVCTesterCPI2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static MVCTesterCPI2.WebStartup;

namespace MVCTesterCPI2.Infrastructure
{
    public class AsyncRequestHost : DelegatingHandler
    {
        private const string _className = "AsyncRequestHost";
        private static CancellationToken cancellationToken;

        public static async Task<HttpResponse<List<T>>> SendRequestForListAsync<T>(HttpRequestMessage request, HttpClient client, string exceptionClientName)
        {
            const string _functionName = "SendRequestForListAsync()";
            try
            {
                //var response = await SendAsync(request, cancellationToken);
                var response = await ExecuteSendRequestAsync(request, client);
                if (response.IsSuccessStatusCode)
                {
                    List<T> jsonResponse = new List<T>();
                    var x = response.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings
                    {
                        DateParseHandling = DateParseHandling.DateTimeOffset
                    };
                    if (x[0] == '"')
                    {
                        string ds = JsonConvert.DeserializeObject<string>(x);
                        JsonConvert.PopulateObject(ds, jsonResponse, settings);
                    }
                    else
                    {
                        JsonConvert.PopulateObject(response.Content.ReadAsStringAsync().Result, jsonResponse, settings);
                    }
                    //with constructor and inheritance
                    //var repoResponse = new HttpResponse<List<T>>(true, response.Content.ReadAsStringAsync().Result, jsonResponse );
                    //without:
                    var httpResponse = new HttpResponse<List<T>> { IsSuccess = true, ResponseContent = jsonResponse };
                    return httpResponse;
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($" { _className}-{_functionName}: Request failed - {client.BaseAddress}{request.RequestUri} - ReqMsg:{response.RequestMessage}");
                    // LoggerLQ.LogQueue($" { _className}-{_functionName}: Request failed - {client.BaseAddress}{request.RequestUri} - ReqMsg:{response.ReasonPhrase}");
                    List<T> jsonResponse = new List<T>();
                    //var repoResponse = new HttpResponse<List<T>>(true, response.Content.ReadAsStringAsync().Result, jsonResponse);
                    var repoResponse = new HttpResponse<List<T>> { IsSuccess = true, ResponseContent = jsonResponse };
                    return repoResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in {_className} - {_functionName}. {exceptionClientName}. Exception: {ex.Message}");
                // LoggerLQ.LogQueue($"Exception in {_className} - {_functionName}. {exceptionClientName}. Exception: {ex.Message}");
                List<T> jsonResponse = new List<T>();
                //var repoResponse = new HttpResponse<List<T>>(true, null, jsonResponse);
                var repoResponse = new HttpResponse<List<T>> { IsSuccess = true, ResponseContent = jsonResponse };
                return repoResponse;
            }
        }

        public static async Task<HttpResponse<T>> SendRequestAsync<T>(HttpRequestMessage request, HttpClient client, string exceptionClientName) where T : new()
        {
            const string _functionName = "SendRequestAsync()";
            try
            {
                var response = await ExecuteSendRequestAsync(request, client);
                if (response.IsSuccessStatusCode)
                {
                    T jsonResponse = new T();
                    //List<T> jrList = new List<T>();
                    var settings = new JsonSerializerSettings
                    {
                        DateParseHandling = DateParseHandling.DateTimeOffset
                    };
                    var jRslt = response.Content.ReadAsStringAsync().Result;
                    //if (jsonResponse.GetType() == typeof(BadgeIntegration.Models.EdFiV3Student))
                    if (jRslt == "[\n]" || jRslt == "[]")
                    {
                        return new HttpResponse<T> { IsSuccess = true, ResponseContent = new T() };
                    }
                    if (jRslt[0] == '[')
                    {
                        var dobj = JsonConvert.DeserializeObject<T[]>(jRslt);
                        var y = dobj.First();
                        var szObj = JsonConvert.SerializeObject(y);
                        JsonConvert.PopulateObject(szObj, jsonResponse, settings);
                    }
                    if (jsonResponse.GetType() == typeof(Login) && jRslt.Contains("Token"))
                    {
                        //string jsonString = (string)JsonConvert.DeserializeObject(jRslt);
                        //JsonConvert.PopulateObject(jsonString, jsonResponse);
                        JObject jsonObject = (JObject)JsonConvert.DeserializeObject(jRslt);
                        JsonConvert.PopulateObject(jsonObject.ToString(), jsonResponse);
                    }
                    else
                    {
                        JsonConvert.PopulateObject(jRslt, jsonResponse);
                    }
                    return new HttpResponse<T> { IsSuccess = true, ResponseContent = jsonResponse };
                    //JsonConvert.PopulateObject(jRslt, jsonResponse);
                    //JsonConvert.PopulateObject(jRslt, jrList);
                    //return new HttpResponse<T> { IsSuccess = true, ResponseContent = jrList[0] != null ? jrList[0] : jrList[1] };
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($" { _className}-{_functionName}: Request failed - {client.BaseAddress}{request.RequestUri} - ReqMsg:{response.RequestMessage}");
                    LoggerLQ.LogQueue($" { _className}-{_functionName}: Request failed - {client.BaseAddress}{request.RequestUri} - ReqMsg:{response.RequestMessage}");
                    return new HttpResponse<T> { IsSuccess = true, StatusCode = HttpStatusCode.BadRequest };
                }
            }
            catch (Exception exc)
            {
                LoggerLQ.LogQueue($"Exception in {_className} at {_functionName}. Exception: {exc.Message}");
                Console.WriteLine($"Exception in {_className} at {_functionName}. Exception: {exc.Message}");
                return new HttpResponse<T> { IsSuccess = true, StatusCode = HttpStatusCode.BadRequest };
            }
        }

        public static async Task<ServerResponse> SendPropagateRequestAsync(HttpRequestMessage request, HttpClient client, string exceptionClientName)
        {
            const string _functionName = "SendPropagateRequestAsync()";
            try
            {
                var response = await ExecuteSendRequestAsync(request, client);
                return new ServerResponse
                {
                    HttpRespMsg = response,
                    Message = await response.Content.ReadAsStringAsync()
                };
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Exception in {_className} at {_functionName}. Exception: {exc.Message}");
                LoggerLQ.LogQueue($"Exception in {_className} at {_functionName}. Exception: {exc.Message}");
                return new ServerResponse
                {
                    HttpRespMsg = new HttpResponseMessage(HttpStatusCode.BadRequest),
                    Message = $"Exception in {_className} at {_functionName}. Exception: {exc.Message}"
                };
            }
        }

        public static async Task<HttpResponseMessage> ExecuteSendRequestAsync(HttpRequestMessage request, HttpClient client)
        {
            try
            {
                var response = await client.SendAsync(request);
                return response;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage reqMsg, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            var response = await Policy.WrapAsync(FallbackPolicy(), RetryPolicy()).ExecuteAsync(() => base.SendAsync(reqMsg, cancellationToken));
            Console.WriteLine($"Completed request in {sw.ElapsedMilliseconds}ms.");
            return response;
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