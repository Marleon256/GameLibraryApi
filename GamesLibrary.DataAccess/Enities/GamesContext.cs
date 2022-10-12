using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace GamesLibrary.DataAccess.Enities
{
    public partial class GamesContext : DbContext
    {
        public GamesContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<DbCompany> Companies { get; set; } = null!;
        public virtual DbSet<DbGame> Games { get; set; } = null!;
        public virtual DbSet<DbGamesMappingGenre> GamesMappingGenres { get; set; } = null!;
        public virtual DbSet<DbGenre> Genres { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbCompany>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Companies_Name")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__Companie__737584F6344BFE38")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<DbGame>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Games_Name")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.CompanyName)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.CompanyNameId)
                    .HasConstraintName("FK__Games__CompanyNa__276EDEB3");
            });

            modelBuilder.Entity<DbGamesMappingGenre>(entity =>
            {
                entity.HasIndex(e => e.GamesId, "IX_GamesMappingGenres_GamesId");

                entity.HasIndex(e => e.GenresId, "IX_GamesMappingGenres_GenresId");

                entity.HasOne(d => d.Games)
                    .WithMany(p => p.GamesMappingGenres)
                    .HasForeignKey(d => d.GamesId)
                    .HasConstraintName("FK__GamesMapp__Games__2D27B809");

                entity.HasOne(d => d.Genres)
                    .WithMany(p => p.GamesMappingGenres)
                    .HasForeignKey(d => d.GenresId)
                    .HasConstraintName("FK__GamesMapp__Genre__2E1BDC42");
            });

            modelBuilder.Entity<DbGenre>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Genres_Name")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__Genres__737584F6D0DF665B")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
