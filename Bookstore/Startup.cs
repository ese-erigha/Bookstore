using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using System.Web.Http;
using Bookstore.Identity;
using Bookstore.Core.Implementations;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.Security.OAuth;
using Bookstore.Providers;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Bookstore;
using Newtonsoft.Json;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using Bookstore.Modules;

[assembly: OwinStartup(typeof(Bookstore.Startup))]

namespace Bookstore
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            HttpConfiguration httpConfig = new HttpConfiguration();

            //IocConfigRegistration
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(httpConfig);

            //Register Modules
            builder.RegisterModule(new EFModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new AutoMapperModule());

            //builder.RegisterType<ApplicationUserManager>().AsSelf();
            //builder.RegisterType<ApplicationRoleManager>().AsSelf();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            httpConfig.DependencyResolver = resolver;

            GlobalConfiguration.Configuration.DependencyResolver = resolver; //Ensures that we can use the dependency resolver anywhere in the application


            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //Database.SetInitializer(new Initializer());

            ConfigureWebApi(httpConfig);

            ConfigureOAuthTokenGeneration(app);

            ConfigureOAuthTokenConsumption(app);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(httpConfig);
            app.UseWebApi(httpConfig);

        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(DatabaseContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // Plugin the OAuth bearer JSON Web Token tokens generation and Consumption will be here
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev environment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/v1/auth/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://localhost:1730")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

        }


        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = "http://localhost:1730";
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
          
            WebApiConfig.Register(config);

            //Return camel-case JSON (thisIsCamelCase), instead of the default pascal-case (ThisIsPascalCase)
            var formatters = config.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}