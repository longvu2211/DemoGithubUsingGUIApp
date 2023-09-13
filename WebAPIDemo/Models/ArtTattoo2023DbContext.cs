using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPIDemo.Models;

public partial class ArtTattoo2023DbContext : DbContext
{
    public ArtTattoo2023DbContext()
    {
    }

    public ArtTattoo2023DbContext(DbContextOptions<ArtTattoo2023DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountMember> AccountMembers { get; set; }

    public virtual DbSet<ArtTattooService> ArtTattooServices { get; set; }

    public virtual DbSet<ArtTattooStyle> ArtTattooStyles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountMember>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__AccountM__349DA58607EF5A4C");

            entity.ToTable("AccountMember");

            entity.HasIndex(e => e.EmailAddress, "UQ__AccountM__49A14740CAF15437").IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.EmailAddress).HasMaxLength(60);
            entity.Property(e => e.FullName).HasMaxLength(60);
            entity.Property(e => e.Password).HasMaxLength(40);
        });

        modelBuilder.Entity<ArtTattooService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__ArtTatto__C51BB00A231DF13A");

            entity.ToTable("ArtTattooService");

            entity.Property(e => e.ServiceId).HasMaxLength(30);
            entity.Property(e => e.ServiceAddress).HasMaxLength(120);
            entity.Property(e => e.ServiceName).HasMaxLength(90);
            entity.Property(e => e.ServiceNote).HasMaxLength(250);
            entity.Property(e => e.TelephoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<ArtTattooStyle>(entity =>
        {
            entity.HasKey(e => e.TattooStyleId).HasName("PK__ArtTatto__C1DFE76AF22809EF");

            entity.ToTable("ArtTattooStyle");

            entity.Property(e => e.TattooStyleId).ValueGeneratedNever();
            entity.Property(e => e.ServiceId).HasMaxLength(30);
            entity.Property(e => e.StyleDescription).HasMaxLength(200);
            entity.Property(e => e.TattooLocation).HasMaxLength(80);
            entity.Property(e => e.TattooStyleName).HasMaxLength(120);

            entity.HasOne(d => d.Service).WithMany(p => p.ArtTattooStyles)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ArtTattoo__Servi__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
