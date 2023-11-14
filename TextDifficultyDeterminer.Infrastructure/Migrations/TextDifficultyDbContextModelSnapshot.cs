﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TextDifficultyDeterminer.Infrastructure.Migrations
{
    [DbContext(typeof(TextDifficultyDbContext))]
    partial class TextDifficultyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("FrequencyWord", b =>
                {
                    b.Property<Guid>("FrequencyWordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("DifficultyScore")
                        .HasColumnType("REAL");

                    b.Property<ulong>("FrequencyOfWord")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Language")
                        .HasColumnType("TEXT");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("FrequencyWordId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });
#pragma warning restore 612, 618
        }
    }
}
