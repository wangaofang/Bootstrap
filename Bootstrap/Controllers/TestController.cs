using Bootstrap.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
    [Route("api/[Controller]")]
    public class TestController:Controller
    {
        private MyContext _context;

        public TestController(MyContext context)
        {
            _context=context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

    }
}