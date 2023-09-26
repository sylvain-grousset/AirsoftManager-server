using AirsoftManager_server.Interface;
using AirsoftManager_server.Models;
using AirsoftManager_server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftManager_server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SessionController : Controller
    {

        public readonly AirsoftManagerContext _context;
        public readonly IEmail _email;
        public readonly IConfiguration _configuration;

        public SessionController(AirsoftManagerContext context, IEmail email, IConfiguration configuration)
        {
            _context = context;
            _email = email;
            _configuration = configuration;
        }

        [HttpGet]
       public IActionResult GetAll()
        {
            AWS_SES aws_ses_conf = _configuration.GetSection("AWS").Get<AWS_SES>();
            _email.SendEmailAsync("sylvain.grousset1@gmail.com" ,QR.GenerateQR(), _email.ConfigureSMTP(aws_ses_conf));
           
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
