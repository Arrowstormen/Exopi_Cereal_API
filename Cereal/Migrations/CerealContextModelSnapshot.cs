﻿// <auto-generated />
using Cereal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cereal.Migrations
{
    [DbContext(typeof(CerealContext))]
    partial class CerealContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("Cereal.Models.CerealEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Calories")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Carbo")
                        .HasColumnType("REAL");

                    b.Property<float>("Cups")
                        .HasColumnType("REAL");

                    b.Property<int>("Fat")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Fiber")
                        .HasColumnType("REAL");

                    b.Property<int>("Manufacturer")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Potass")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Protein")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Rating")
                        .HasColumnType("REAL");

                    b.Property<int>("Shelf")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sodium")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sugars")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Vitamins")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Cereals");
                });
#pragma warning restore 612, 618
        }
    }
}
