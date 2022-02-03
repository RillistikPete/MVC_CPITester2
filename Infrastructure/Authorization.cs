using MVCTesterCPI2.Infrastructure;
using MVCTesterCPI2.Infrastructure.DbSetup;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVCTesterCPI2.Models
{
    public class Authorization
    {
        private static readonly string apiLoginUri = ConfigurationManager.AppSettings["CpiLoginUri"];
        private static readonly string cpiBaseUri = ConfigurationManager.AppSettings["CpiBaseUri"];
        private static readonly string _className = "Authorization";
        public static long userID;
        public static string _cpiToken;
        public static HttpClient _cpiClient;
        private static readonly HttpClientHandler _clientHandler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
        };
        private static HttpClient OAuthClient = new HttpClient(_clientHandler);

        public static void InitiateClients(IHttpClientFactory httpClientFactory)
        {
            _cpiClient = httpClientFactory.CreateClient("cpiClient");
        }

        public static async Task<Login> GetCpiToken()
        {
            int notFound = 404;
            int badRequest = 400;
            string _functionName = "GetCpiToken";

            // devnet
            var userObj = new Models.CPIUser()
            {
                ProgramID = 1,
                CPIUsername = ConfigurationManager.AppSettings["Username"],
                Password = ConfigurationManager.AppSettings["password"]
            };

            //Using ByteArray for passing string content to body of request:

            // var httpContent = JsonConvert.SerializeObject(userObj);
            // var buffer = Encoding.UTF8.GetBytes(httpContent);
            // var byteContent = new ByteArrayContent(buffer);
            // byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, cpiBaseUri + apiLoginUri)
            // {
            //     Content = byteContent
            // };
            // request.Headers.Add("Authorization", "Basic ");

            Athenaeum ath = new Athenaeum(new CpiClientBase(_cpiClient));
            try
            {
                HttpResponse<Login> response = await ath.Login(cpiBaseUri + apiLoginUri, userObj, null);
                if (response.ResponseContent != null)
                {
                    userID = response.ResponseContent.UserId;
                    return response.ResponseContent;
                }
                else
                {
                    throw new HttpException(badRequest, "Bad Request");
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine($"Exception in {_className} at {_functionName}: {notFound} - {exc.Message}");
                LoggerLQ.LogQueue($"Exception in {_className} at {_functionName}: {notFound} - {exc.Message}");
                throw;
            }
        }

        public static async Task FillTokens()
        {
            if(_cpiToken == null)
            {
                var loginObject = await GetCpiToken();
                _cpiToken = loginObject.Token;
                UpdateAuthorizations();
            }
        }

        public static void UpdateAuthorizations()
        {
            _cpiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cpiToken);
        }
    }
}
