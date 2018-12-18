using System.Data.Entity;
using Bookstore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web;
using System.Web.Http;
using Bookstore.Core.Implementations;

namespace Bookstore
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //IocConfig.Register();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //Database.SetInitializer(new Initializer());

            //Return camel-case JSON (thisIsCamelCase), instead of the default pascal-case (ThisIsPascalCase)
            //var formatters = GlobalConfiguration.Configuration.Formatters;
            //var jsonFormatter = formatters.JsonFormatter;
            //var settings = jsonFormatter.SerializerSettings;
            //settings.Formatting = Formatting.Indented;
           //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
