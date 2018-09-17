﻿// <auto-generated />
using AdminPanelWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdminPanelWebApp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180917084940_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("AdminPanelWebApp.Models.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Difficulty");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("QuestionContent")
                        .IsRequired();

                    b.Property<string>("ResponseContent")
                        .IsRequired();

                    b.Property<int>("Subject");

                    b.HasKey("ID");

                    b.ToTable("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
