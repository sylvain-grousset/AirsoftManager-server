using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirsoftManager_server.Models;

[PrimaryKey(nameof(SessionId), nameof(Participant_id))]
[Table("session_participant")]
public partial class SessionParticipant
{
    [Column("session_id")]
    public int SessionId { get; set; }

    [Column("participant_id")]
    public int Participant_id { get; set; }
}
