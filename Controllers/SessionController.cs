using AirsoftManager_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftManager_server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SessionController : Controller
    {

        public readonly AirsoftManagerContext _context;

        public SessionController(AirsoftManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
       public IActionResult GetAll()
        {
            return Ok(_context.Sessions.OrderBy(t => t.SessionDate).ThenBy(t => t.HeureDebut).ToList());
        }

    }
}
