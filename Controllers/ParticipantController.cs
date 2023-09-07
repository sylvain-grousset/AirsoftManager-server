using AirsoftManager_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftManager_server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ParticipantController : Controller
    {

        private readonly AirsoftManagerContext _context;

        public ParticipantController(AirsoftManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Participants.ToList());
        }
    }
}
