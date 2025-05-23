﻿// <auto-generated />
using System;
using Hexata.Infrastructure.SqlLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hexata.Infrastructure.SqlLite.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250428194055_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("Hexata.BI.Application.Entities.ServiceState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Error")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastSync")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ServiceStates");
                });
#pragma warning restore 612, 618
        }
    }
}
