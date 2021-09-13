using System.Net.Http;

namespace MVCTesterCPI2.Infrastructure
{
    public class ServerResponse
    {
        public HttpResponseMessage HttpRespMsg { get; set; }
        public string Message { get; set; }
    }
}