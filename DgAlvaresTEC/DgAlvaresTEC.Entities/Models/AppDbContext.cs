using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DgAlvaresTEC.Entities.Models
{
    //mapstring dotnet ef dbcontext scaffold "Server=localhost;DataBase=dg_auth;Uid=ADMIN;Pwd=dgalvarestecadmin"  Pomelo.EntityFrameworkCore.MySql -o Models -f -c AppDbContext --project .\DgAlvaresTEC.Api\DgAlvaresTEC.Api.csproj
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AutPessoa> AutPessoa { get; set; }
        public virtual DbSet<AutTbgen> AutTbgen { get; set; }
        public virtual DbSet<AutUsuario> AutUsuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;database=dg_auth;uid=ADMIN;pwd=dgalvarestecadmin", x => x.ServerVersion("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutPessoa>(entity =>
            {
                entity.ToTable("aut_pessoa");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Status)
                    .HasName("pessoa_status_fk_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.AutPessoa)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pessoa_status_fk");
            });

            modelBuilder.Entity<AutTbgen>(entity =>
            {
                entity.ToTable("aut_tbgen");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Ordem).HasColumnName("ordem");

                entity.Property(e => e.Tipo).HasColumnName("tipo");
            });

            modelBuilder.Entity<AutUsuario>(entity =>
            {
                entity.ToTable("aut_usuario");

                entity.HasComment("tabelas de usuarios dos sistemas dgalvares tec");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Status)
                    .HasName("usuario_status_fk_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.AutUsuario)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuario_status_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
