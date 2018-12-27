using Bookstore.CustomHandler.Interfaces;
using Bookstore.Entities.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Bookstore.Service.Interfaces;

namespace Bookstore.CustomHandler.Implementations
{
    public class RequestResponseHandler : DelegatingHandler
    {
        private readonly IMessageLoggerService _messageLoggerService;

        public RequestResponseHandler(IMessageLoggerService messageLoggerService)
        {
            _messageLoggerService = messageLoggerService;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
        {
            var requestedMethod = request.Method;
            var userHostAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "0.0.0.0";
            var useragent = request.Headers.UserAgent.ToString();
            var requestMessage = await request.Content.ReadAsByteArrayAsync();
            var uriAccessed = request.RequestUri.AbsoluteUri;

            var responseHeadersString = new StringBuilder();
            foreach (var header in request.Headers)
            {
                responseHeadersString.Append($"{header.Key}: {String.Join(", ", header.Value)}{Environment.NewLine}");
            }

            

            var requestLog = new ApiLog()
            {
                Headers = responseHeadersString.ToString(),
                AbsoluteUri = uriAccessed,
                Host = userHostAddress,
                RequestBody = Encoding.UTF8.GetString(requestMessage),
                UserHostAddress = userHostAddress,
                Useragent = useragent,
                RequestType = "Request",
                RequestedMethod = requestedMethod.ToString(),
                StatusCode = string.Empty
            };

            _messageLoggerService.LogMessageAsync(requestLog);

            var response = await base.SendAsync(request, cancellationToken);

            byte[] responseMessage;
            if (response.IsSuccessStatusCode)
                responseMessage = await response.Content.ReadAsByteArrayAsync();
            else
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

            var responseLog = new ApiLog()
            {
                Headers = responseHeadersString.ToString(),
                AbsoluteUri = uriAccessed,
                Host = userHostAddress,
                RequestBody = Encoding.UTF8.GetString(responseMessage),
                UserHostAddress = userHostAddress,
                Useragent = useragent,
                RequestType = "Response",
                RequestedMethod = requestedMethod.ToString(),
                StatusCode = string.Empty
            };

            _messageLoggerService.LogMessageAsync(responseLog);
            return response;
        }
    }
}