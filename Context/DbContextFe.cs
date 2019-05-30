using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace apiEsFeDemostracion.Entities
{
    public partial class DbContextFe : DbContext
    {
        public DbContextFe()
        {
        }

        public DbContextFe(DbContextOptions<DbContextFe> options)
            : base(options)
        {
        }

        public virtual DbSet<EFacAcceso> EFacAcceso { get; set; }
 
        public virtual DbSet<EFacMenu> EFacMenu { get; set; }

        public virtual DbSet<EFacRol> EFacRol { get; set; }

        public virtual DbSet<EFacUsuario> EFacUsuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=sql7004.site4now.net;Initial Catalog=DB_A3F844_20601244676;User ID=DB_A3F844_20601244676_admin;Password=essolution1230.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EFacAcceso>(entity =>
            {
                entity.HasKey(e => new { e.AccesoId, e.RucCia, e.RolId, e.MenuId });

                entity.ToTable("eFacAcceso");

                entity.Property(e => e.AccesoId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RucCia)
                    .HasColumnName("RUC_Cia")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.RolId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.EFacAcceso)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_eFacAcceso_eFacMenu");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.EFacAcceso)
                    .HasForeignKey(d => new { d.RolId, d.RucCia })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_eFacAcceso_eFacRol");
            });


            modelBuilder.Entity<EFacMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("eFacMenu");

                entity.Property(e => e.MenuId).ValueGeneratedNever();

                entity.Property(e => e.EstadoMnu).HasColumnName("Estado_Mnu");

                entity.Property(e => e.HtmlMnu)
                    .HasColumnName("Html_Mnu")
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.NombreMnu)
                    .HasColumnName("Nombre_Mnu")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrdenMnu).HasColumnName("Orden_Mnu");

                entity.Property(e => e.PadreMnu).HasColumnName("Padre_Mnu");

                entity.Property(e => e.VisibleMnu).HasColumnName("Visible_Mnu");
            });

            modelBuilder.Entity<EFacRol>(entity =>
            {
                entity.HasKey(e => new { e.RolId, e.RucCia });

                entity.ToTable("eFacRol");

                entity.Property(e => e.RolId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RucCia)
                    .HasColumnName("RUC_Cia")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ActivoRol).HasColumnName("Activo_Rol");

                entity.Property(e => e.NombreRol)
                    .HasColumnName("Nombre_Rol")
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<EFacUsuario>(entity =>
            {
                entity.HasKey(e => new { e.UsuarioId, e.RucCia });

                entity.ToTable("eFacUsuario");

                entity.Property(e => e.UsuarioId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RucCia)
                    .HasColumnName("RUC_Cia")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ActivoUsu).HasColumnName("Activo_Usu");

                entity.Property(e => e.AdministradorUsu).HasColumnName("Administrador_Usu");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoElectronicoUsu)
                    .HasColumnName("CorreoElectronico_Usu")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsu)
                    .HasColumnName("Nombre_Usu")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioWindows)
                    .HasColumnName("Usuario_Windows")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
