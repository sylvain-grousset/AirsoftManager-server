using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirsoftManager_server.Models;

public partial class AirsoftManagerContext : DbContext
{
    public AirsoftManagerContext()
    {
    }

    public AirsoftManagerContext(DbContextOptions<AirsoftManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionParticipant> SessionParticipants { get; set; }


    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.ParticipantId).HasName("participant_pkey");

            entity.ToTable("participant");

            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(255)
                .HasColumnName("prenom");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("session_pkey");

            entity.ToTable("session");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.HeureDebut).HasColumnName("heure_debut");
            entity.Property(e => e.HeureFin).HasColumnName("heure_fin");
            entity.Property(e => e.MaxParticipants).HasColumnName("max_participants");
            entity.Property(e => e.SessionDate).HasColumnName("session_date");
        });

        modelBuilder.Entity<SessionParticipant>(entity =>
        {
            entity.HasKey(e => new { e.SessionId, e.Email }).HasName("session_participant_pkey");

            entity.ToTable("session_participant");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
        });

        OnModelCreatingPartial(modelBuilder);
    }*/

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
