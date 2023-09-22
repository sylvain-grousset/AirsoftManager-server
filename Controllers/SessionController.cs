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
            var result = _context.Sessions
                         .GroupJoin(
                             _context.SessionParticipants,
                             s => s.SessionId,
                             sp => sp.SessionId,
                             (s, participants) => new
                             {
                                 Session = s,
                                 NumberOfParticipants = participants.Count()
                             })
                         .OrderBy(r => r.Session.SessionDate)
                         .ThenBy(r => r.Session.HeureDebut)
                         .ToList();
            return Ok(result);
        }

    }
}
