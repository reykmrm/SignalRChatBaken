using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SignalRChatGPT.Modelos
{
    public partial class ChatBacapContext : DbContext
    {
       // PM> Scaffold-DbContext "Data Source=DESKTOP-PA7T1D5\SQLEXPRESS;Initial Catalog=ChatBacap;User ID=sebas;Password=123456;Connect Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Modelos -force
        public ChatBacapContext()
        {
        }

        public ChatBacapContext(DbContextOptions<ChatBacapContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Grupo> Grupos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuariosAmigo> UsuariosAmigos { get; set; } = null!;
        public virtual DbSet<UsuariosGrupo> UsuariosGrupos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-PA7T1D5\\SQLEXPRESS;Initial Catalog=ChatBacap;User ID=sebas;Password=123456;Connect Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Usuario1).HasColumnName("Usuario");
            });

            modelBuilder.Entity<UsuariosAmigo>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdUsuario1Navigation)
                    .WithMany(p => p.UsuariosAmigoIdUsuario1Navigations)
                    .HasForeignKey(d => d.IdUsuario1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuariosA__IdUsu__5441852A");

                entity.HasOne(d => d.IdUsuario2Navigation)
                    .WithMany(p => p.UsuariosAmigoIdUsuario2Navigations)
                    .HasForeignKey(d => d.IdUsuario2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuariosA__IdUsu__5535A963");
            });

            modelBuilder.Entity<UsuariosGrupo>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.UsuariosGrupos)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuariosG__IdGru__571DF1D5");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuariosGrupos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuariosG__IdUsu__5629CD9C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
