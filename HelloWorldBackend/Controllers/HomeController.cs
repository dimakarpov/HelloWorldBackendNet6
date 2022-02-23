using Microsoft.AspNetCore.Mvc;

namespace HelloWorldBackend.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet(Name = "Index")]
        public string Index()
        {
            return "Hello World";
        }
    }
}
