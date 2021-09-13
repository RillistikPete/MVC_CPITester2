using System.Net.Http;

namespace MVCTesterCPI2.Infrastructure
{
    public class HttpResponse<T> : HttpResponseMessage
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T ResponseContent { get; set; }
    }
}