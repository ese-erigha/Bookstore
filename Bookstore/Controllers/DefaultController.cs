using System;
using System.Web.Http;

namespace Bookstore.Controllers
{
    [RoutePrefix("api/v1/default")]
    public class DefaultController: ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Content(System.Net.HttpStatusCode.OK, "Hello");
        }
    }
}
