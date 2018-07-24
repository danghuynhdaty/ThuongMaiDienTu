using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using System.Web.Http;

namespace OnlineShop.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : BaseApiController
    {
        IErrorService _errorService;
        public HomeController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello, TEDU Member. ";
        }
    }
}