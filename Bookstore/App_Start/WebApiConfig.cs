using Bookstore.CustomHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using Bookstore.CustomHandler.Implementations;
using Bookstore.CustomHandler.Interfaces;
using Microsoft.Web.Http;
using Microsoft.Web.Http.Versioning;

namespace Bookstore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MessageHandlers.Add(new PreflightRequestsHandler());

            config.AddApiVersioning(o =>
                {
                    o.ReportApiVersions = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.ApiVersionReader = new HeaderApiVersionReader("version");
                    o.ApiVersionSelector = new CurrentImplementationApiVersionSelector(o);
                }
            );

            // Web API routes
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var scope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
            IGlobalExceptionHandler globalExceptionHandler = scope.GetService(typeof(IGlobalExceptionHandler)) as IGlobalExceptionHandler;
            IUnhandledExceptionLogger unhandledExceptionLogger = scope.GetService(typeof(IUnhandledExceptionLogger)) as IUnhandledExceptionLogger;
            RequestResponseHandler messageHandler = scope.GetService(typeof(RequestResponseHandler)) as RequestResponseHandler;

            config.Services.Replace(typeof(IExceptionHandler), globalExceptionHandler);
            config.Services.Replace(typeof(IExceptionLogger),  unhandledExceptionLogger);

            config.MessageHandlers.Add(messageHandler);

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //config.Formatters.Remove(config.Formatters.XmlFormatter); //Remove XML Formatting from Web API

            /*config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/v1/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );*/
        }
    }


    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory>
        GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>
            (inherit: true);
        }
    }

    public class PreflightRequestsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("Origin") && request.Method.Method == "OPTIONS")
            {
                var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
                response.Headers.Add("Access-Control-Allow-Origin", "*");

                response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept, Authorization");
                response.Headers.Add("Access-Control-Allow-Methods", "*");
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
