﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SDQRealEstate.Infrastructure.Persistence.Contexts;

#nullable disable

namespace SDQRealEstate.Infrastucture.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.Favorita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdPropiedad")
                        .HasColumnType("int");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdPropiedad");

                    b.ToTable("Favorita", (string)null);
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.Fotos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropiedadId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PropiedadId");

                    b.ToTable("Fotos", (string)null);
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.Mejora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mejora", (string)null);
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.Propiedad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantBanos")
                        .HasColumnType("int");

                    b.Property<int>("CantHabitaciones")
                        .HasColumnType("int");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MejorasId")
                        .HasColumnType("int");

                    b.Property<int>("Metros")
                        .HasColumnType("int");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.Property<int>("TipoPropiedadId")
                        .HasColumnType("int");

                    b.Property<int>("TipoVentaId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MejorasId");

                    b.HasIndex("TipoPropiedadId");

                    b.HasIndex("TipoVentaId");

                    b.ToTable("Propiedad", (string)null);
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.TipoPropiedades", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoPropiedades", (string)null);
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.TipoVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoVenta", (string)null);
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.Favorita", b =>
                {
                    b.HasOne("SDQRealEstate.Core.Domain.Entities.Propiedad", "propiedad")
                        .WithMany("favorita")
                        .HasForeignKey("IdPropiedad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("propiedad");
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.Fotos", b =>
                {
                    b.HasOne("SDQRealEstate.Core.Domain.Entities.Propiedad", "propiedad")
                        .WithMany("fotos")
                        .HasForeignKey("PropiedadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("propiedad");
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.Propiedad", b =>
                {
                    b.HasOne("SDQRealEstate.Core.Domain.Entities.Mejora", "Mejoras")
                        .WithMany("propiedad")
                        .HasForeignKey("MejorasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SDQRealEstate.Core.Domain.Entities.TipoPropiedades", "tipoPropiedades")
                        .WithMany("propiedad")
                        .HasForeignKey("TipoPropiedadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SDQRealEstate.Core.Domain.Entities.TipoVenta", "tipoVenta")
                        .WithMany("propiedad")
                        .HasForeignKey("TipoVentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mejoras");

                    b.Navigation("tipoPropiedades");

                    b.Navigation("tipoVenta");
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.Mejora", b =>
                {
                    b.Navigation("propiedad");
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.Propiedad", b =>
                {
                    b.Navigation("favorita");

                    b.Navigation("fotos");
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.TipoPropiedades", b =>
                {
                    b.Navigation("propiedad");
                });

            modelBuilder.Entity("SDQRealEstate.Core.Domain.Entities.TipoVenta", b =>
                {
                    b.Navigation("propiedad");
                });
#pragma warning restore 612, 618
        }
    }
}
