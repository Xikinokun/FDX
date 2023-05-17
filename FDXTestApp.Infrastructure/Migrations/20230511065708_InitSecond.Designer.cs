﻿// <auto-generated />
using System;
using FDXTestApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FDXTestApp.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230511065708_InitSecond")]
    partial class InitSecond
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FDXTestApp.Domain.Entities.Recipient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SmsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SmsId");

                    b.ToTable("Recipient");
                });

            modelBuilder.Entity("FDXTestApp.Domain.Entities.Sms", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sms");
                });

            modelBuilder.Entity("FDXTestApp.Domain.Entities.Recipient", b =>
                {
                    b.HasOne("FDXTestApp.Domain.Entities.Sms", "Sms")
                        .WithMany("To")
                        .HasForeignKey("SmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sms");
                });

            modelBuilder.Entity("FDXTestApp.Domain.Entities.Sms", b =>
                {
                    b.Navigation("To");
                });
#pragma warning restore 612, 618
        }
    }
}
