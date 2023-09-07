using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirsoftManager_server.Models;

[PrimaryKey(nameof(SessionId))]
[Table("session")]
public partial class Session
{
    [Column("session_id")]
    public int SessionId { get; set; }

    [Column("session_date")]
    public DateOnly SessionDate { get; set; }

    [Column("heure_debut")]
    public TimeOnly HeureDebut { get; set; }

    [Column("heure_fin")]
    public TimeOnly HeureFin { get; set; }

    [Column("max_participants")]
    public short MaxParticipants { get; set; }

    [Column("description")]
    public string? Description { get; set; }
}
