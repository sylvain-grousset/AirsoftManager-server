using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirsoftManager_server.Models;

[PrimaryKey(nameof(ParticipantId))]
[Table("participant")]
public partial class Participant
{
    [Column("participant_id")]
    public int ParticipantId { get; set; }

    [Column("nom")]
    [MaxLength(255)]
    public string Nom { get; set; } = null!;

    [Column("prenom")]
    [MaxLength(255)]
    public string Prenom { get; set; } = null!;

    [Column("email")]
    [MaxLength(255)]
    public string Email { get; set; } = null!;
}
