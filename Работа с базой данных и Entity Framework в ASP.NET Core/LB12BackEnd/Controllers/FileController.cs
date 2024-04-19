using LB12BackEnd.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB10Back.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class FileController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public FileController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetHtml()
        {
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }
    }
}
