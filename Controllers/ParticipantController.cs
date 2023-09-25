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

        [HttpPost]
        public IActionResult InscriptionParticipant(InscriptionParticipantModel leParticipant)
        {
            //Si le participant s'insrit pour la première fois, alors on le crée dans la base.
            Participant participant = _context.Participants.Where(t => t.Email == leParticipant.email).FirstOrDefault();
            
            if(participant == null)
            {
                participant = new Participant
                {
                    Prenom = leParticipant.prenom,
                    Nom = leParticipant.nom,
                    Email = leParticipant.email
                };

                _context.Participants.Add(participant);
                _context.SaveChanges();
            }

            //Le participant ne peut pas s'inscrire plusieurs fois à la même session.
            SessionParticipant isAlreadyRegistered = _context.SessionParticipants.Where(t => t.Participant_id == participant.ParticipantId && t.SessionId == leParticipant.sessionID).FirstOrDefault();

            if(isAlreadyRegistered == null)
            {
                return Ok(AddParticipantToSession(leParticipant.sessionID, participant.ParticipantId));
            }
            else
            {
                return Json("Already registered");
            }

        }

        private bool AddParticipantToSession(int sessionID, int ParticipantID)
        {
            try
            {
                SessionParticipant sessionParticipant = new SessionParticipant
                {
                    Participant_id = ParticipantID,
                    SessionId = sessionID,
                    IsScanned = false,
                };

                _context.SessionParticipants.Add(sessionParticipant);
                _context.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public record InscriptionParticipantModel(int sessionID, string nom, string prenom, string email);
    }
}
