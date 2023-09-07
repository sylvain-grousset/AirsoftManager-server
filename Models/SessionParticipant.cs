using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AirsoftManager_server.Models;

[PrimaryKey(nameof(SessionId), nameof(Participant_id))]

public partial class SessionParticipant
{
    public int SessionId { get; set; }

    public int Participant_id { get; set; }
}
