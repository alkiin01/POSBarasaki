using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POSBarasaki.Models;

public partial class EpicPosContext : DbContext
{
    public EpicPosContext()
    {
    }

    public EpicPosContext(DbContextOptions<EpicPosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MOZAMBIQUE;Database=EpicPos;User Id=sa;Password=sa;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.GroupCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
