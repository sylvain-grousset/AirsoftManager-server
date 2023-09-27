using AirsoftManager_server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftManager_server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SessionParticipantController : Controller
    {

        private readonly AirsoftManagerContext _context;

        public SessionParticipantController(AirsoftManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("~/[action]")]
        public IActionResult SetIsScanned([FromQuery]int sessionid, [FromQuery]int participantid)
        {
            //À déterminer. Si le UserAgent est différent de celui utilisé pour faire la requête via l'appli mobile,
            //              alors ça veut dire qu'un participant a scanné un QRCode donc on ne met pas à jour.
            /*if(Request.Headers.UserAgent != "")
            {

            }*/

            SessionParticipant participantScanned = _context.SessionParticipants.Where(t => t.Participant_id == participantid && t.SessionId == sessionid).FirstOrDefault();
            participantScanned.IsScanned = true;

            _context.SessionParticipants.Update(participantScanned);
            _context.SaveChanges();
            return Ok();
        }
    }
}
