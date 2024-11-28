﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vila_WebAPI.Context;

#nullable disable

namespace Vila_WebAPI.Migrations
{
    [DbContext(typeof(VilaContext))]
    [Migration("20241125151327_edite_vilatbl")]
    partial class edite_vilatbl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vila_WebAPI.Models.Detail", b =>
                {
                    b.Property<int>("DeyailaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeyailaId"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("VilaId")
                        .HasColumnType("int");

                    b.Property<string>("What")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.HasKey("DeyailaId");

                    b.HasIndex("VilaId");

                    b.ToTable("details");
                });

            modelBuilder.Entity("Vila_WebAPI.Models.Vila", b =>
                {
                    b.Property<int>("VilaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VilaID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("MadeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<long>("SellPrice")
                        .HasColumnType("bigint");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<long>("dayPrice")
                        .HasColumnType("bigint");

                    b.HasKey("VilaID");

                    b.ToTable("vilas");
                });

            modelBuilder.Entity("Vila_WebAPI.Models.Detail", b =>
                {
                    b.HasOne("Vila_WebAPI.Models.Vila", "vila")
                        .WithMany("details")
                        .HasForeignKey("VilaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("vila");
                });

            modelBuilder.Entity("Vila_WebAPI.Models.Vila", b =>
                {
                    b.Navigation("details");
                });
#pragma warning restore 612, 618
        }
    }
}
