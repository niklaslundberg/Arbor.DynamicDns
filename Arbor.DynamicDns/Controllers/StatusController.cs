using Microsoft.AspNetCore.Mvc;

namespace Arbor.DynamicDns.Controllers
{
    public class StatusController : Controller
    {
        [HttpGet]
        [Route("")]
        public object Index([FromServices] DnsStatus status)
        {
            return status;
        }
    }
}