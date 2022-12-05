using Microsoft.EntityFrameworkCore;
using SDQRealEstate.Core.Domain.Common;
using SDQRealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SDQRealEstate.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Propiedad> Propiedad { get; set; }
        public DbSet<Fotos> Fotos { get; set; }
        public DbSet<Mejora> Mejora { get; set; }
        public DbSet<TipoPropiedades> TipoPropiedades { get; set; }
        public DbSet<TipoVenta> TipoVenta { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region tables

            modelBuilder.Entity<Propiedad>()
                .ToTable("Propiedad");

            modelBuilder.Entity<Fotos>()
                .ToTable("Fotos");

            modelBuilder.Entity<Mejora>()
                .ToTable("Mejora");

            modelBuilder.Entity<TipoPropiedades>()
                .ToTable("TipoPropiedades");

            modelBuilder.Entity<TipoVenta>()
                .ToTable("TipoVenta");

            #endregion

            #region "primary keys"
            modelBuilder.Entity<Propiedad>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Fotos>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Mejora>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<TipoPropiedades>()
               .HasKey(f => f.Id);

            modelBuilder.Entity<TipoVenta>()
               .HasKey(f => f.Id);
            #endregion

            #region "Relationships"

            modelBuilder.Entity<Propiedad>()
            .HasMany<Fotos>(f => f.fotos)
            .WithOne(P => P.propiedad)
            .HasForeignKey(p => p.PropiedadId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TipoVenta>()
            .HasMany<Propiedad>(f => f.propiedad)
            .WithOne(P => P.tipoVenta)
            .HasForeignKey(p => p.TipoVentaId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TipoPropiedades>()
            .HasMany<Propiedad>(f => f.propiedad)
            .WithOne(P => P.tipoPropiedades)
            .HasForeignKey(p => p.TipoPropiedadId)
            .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region "Property configurations"

            #region Propiedad

            modelBuilder.Entity<Propiedad>().
                Property(p => p.UserId)
                .IsRequired();

            modelBuilder.Entity<Propiedad>().
               Property(p => p.Metros)
               .IsRequired();

            modelBuilder.Entity<Propiedad>().
               Property(p => p.CantBanos)
               .IsRequired();

                modelBuilder.Entity<Propiedad>().
               Property(p => p.ImgUrl)
               .IsRequired();

            modelBuilder.Entity<Propiedad>().
             Property(p => p.CantHabitaciones)
             .IsRequired();

            modelBuilder.Entity<Propiedad>().
             Property(p => p.TipoVentaId).IsRequired();

            modelBuilder.Entity<Propiedad>().
               Property(p => p.TipoPropiedadId)
               .IsRequired();

            modelBuilder.Entity<Propiedad>().
               Property(p => p.Precio)
               .IsRequired();

            modelBuilder.Entity<Propiedad>().
               Property(p => p.Descripcion)
               .IsRequired();

            #endregion

            #region Fotos
            modelBuilder.Entity<Fotos>().
              Property(c => c.PropiedadId)
              .IsRequired();

            modelBuilder.Entity<Fotos>().
             Property(c => c.UserId)
             .IsRequired();

            modelBuilder.Entity<Fotos>().
            Property(c => c.ImageUrl)
            .IsRequired();
            #endregion

            #region Mejora
            modelBuilder.Entity<Mejora>().
              Property(c => c.Name)
              .IsRequired();

            modelBuilder.Entity<Mejora>().
             Property(c => c.Description)
             .IsRequired();
            #endregion

            #region TipoVenta
            modelBuilder.Entity<Mejora>().
              Property(c => c.Name)
              .IsRequired();

            modelBuilder.Entity<Mejora>().
             Property(c => c.Description)
             .IsRequired();
            #endregion

            #region TipoPropiedades
            modelBuilder.Entity<Mejora>().
              Property(c => c.Name)
              .IsRequired();

            modelBuilder.Entity<Mejora>().
             Property(c => c.Description)
             .IsRequired();

            #endregion

            #endregion

        }

    }
}
