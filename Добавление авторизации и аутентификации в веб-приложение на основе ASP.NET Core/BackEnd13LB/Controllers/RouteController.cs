using Microsoft.AspNetCore.Mvc;

namespace BackEnd13LB.Controllers
{
    public class RouteController : Controller
    {
        [Route("NotHome/index")]
        public IActionResult Index()
        {
            return Ok("Вы попали на неизвестную страницу");
        }

        [Route("NotHome/base")]
        public IActionResult Base()
        {
            return Ok("Базовая страница");
        }
    }
}
