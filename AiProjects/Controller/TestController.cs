using AiProjects.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiProjects.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                _context.Database.CanConnect();
                return Ok("Connected to the database!");
            }
            catch
            {
                return BadRequest("Failed to connect to the database!");
            }
        }
    }
}
