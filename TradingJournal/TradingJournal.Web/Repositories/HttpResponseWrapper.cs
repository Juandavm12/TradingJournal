using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradingJournal.Web.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        public bool Error { get; set; }

        public T Response { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }

        public async Task<string> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }

            var StatusCode = HttpResponseMessage.StatusCode;
            if (StatusCode == HttpStatusCode.NotFound)
            {
                return "Resource not found";
            }
            else if (StatusCode == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if (StatusCode == HttpStatusCode.Unauthorized)
            {
                return " Must be logged in to perform this action";
            }
            else if (StatusCode == HttpStatusCode.Forbidden)
            {
                return " You are not allowed to perform this action";
            }

            return "An unexpected error has occurred";
        }
    }
}
