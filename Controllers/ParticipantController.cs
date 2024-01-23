using AirsoftManager_server.Models;
using Microsoft.AspNetCore.Mvc;
using AirsoftManager_server.Utils;
using AirsoftManager_server.Interface;

namespace AirsoftManager_server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ParticipantController : Controller
    {

        private readonly AirsoftManagerContext _context;
        public readonly IEmail _email;
        public readonly IConfiguration _configuration;

        public ParticipantController(AirsoftManagerContext context, IEmail email, IConfiguration configuration)
        {
            _context = context;
            _email = email;
            _configuration = configuration;
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
                AWS_SES aws_ses_conf = _configuration.GetSection("AWS").Get<AWS_SES>();
                _email.SendEmailAsync("sylvain.grousset1@gmail.com", QR.GenerateQR(), _email.ConfigureSMTP(aws_ses_conf));

                AddParticipantToSession(leParticipant.sessionID, participant.ParticipantId);
                return Json(QR.GenerateQR());
            }
            else
            {
                return Json("Already registered");
            }

        }

        [HttpGet]
        public IActionResult GetParticipantStatus(int sessionID, bool isScanned)
        {
            List<Participant> lesParticipants = _context.SessionParticipants
                .Join(_context.Participants, p => p.Participant_id, sp => sp.ParticipantId,
                    (sp, participant) => new
                    {
                        sp,
                        participant
                    })
                .Where(t => t.sp.SessionId == sessionID && t.sp.IsScanned == isScanned)
                .Select(t => t.participant).ToList();

            return Ok(lesParticipants);
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
