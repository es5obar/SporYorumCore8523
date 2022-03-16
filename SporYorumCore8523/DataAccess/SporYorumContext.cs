using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SporYorumCore8523.DataAccess
{
    public partial class SporYorumContext : DbContext
    {
        public SporYorumContext()
        {
        }

        public SporYorumContext(DbContextOptions<SporYorumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Spor> Spor { get; set; } = null!;
        public virtual DbSet<Yorum> Yorum { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\;Database=SporYorum;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spor>(entity =>
            {
                entity.Property(e => e.TakimAdi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TakimUlke)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Yorum>(entity =>
            {
                entity.Property(e => e.Icerik)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Yorumcu)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Spor)
                    .WithMany(p => p.Yorum)
                    .HasForeignKey(d => d.SporId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Yorum_Spor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
