﻿// <auto-generated />
using GalaxyMapSiteApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GalaxyMapSiteApi.Migrations
{
    [DbContext(typeof(GalaxyMapContext))]
    [Migration("20250217174246_TEST")]
    partial class TEST
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GalaxyMapSiteApi.Models.Spacelane", b =>
                {
                    b.Property<string>("DestinationId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Focus")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("DestinationId");

                    b.HasIndex("OriginId");

                    b.ToTable("spacelanes");
                });

            modelBuilder.Entity("GalaxyMapSiteApi.Models.System", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Focus")
                        .HasColumnType("integer");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sector")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("Name");

                    b.ToTable("solar_systems");
                });

            modelBuilder.Entity("GalaxyMapSiteApi.Models.Spacelane", b =>
                {
                    b.HasOne("GalaxyMapSiteApi.Models.System", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GalaxyMapSiteApi.Models.System", "Origin")
                        .WithMany()
                        .HasForeignKey("OriginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");

                    b.Navigation("Origin");
                });
#pragma warning restore 612, 618
        }
    }
}
