﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestoMVC.Data;

#nullable disable

namespace TestoMVC.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20230208201914_aa")]
    partial class aa
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestoMVC.Models.CaseStudies", b =>
                {
                    b.Property<int>("CaseStudyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CaseStudyId"));

                    b.Property<DateTime>("CaseCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CaseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuideLines")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Questionnaires")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartingPoint")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseStudyId");

                    b.ToTable("CaseStudies");
                });

            modelBuilder.Entity("TestoMVC.Models.TestCases", b =>
                {
                    b.Property<int>("TestCaseNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestCaseNumber"));

                    b.Property<string>("TestCaseLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestCaseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("isTestCaseDone")
                        .HasColumnType("bit");

                    b.HasKey("TestCaseNumber");

                    b.ToTable("TestsCases");
                });

            modelBuilder.Entity("TestoMVC.Models.TestResults", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResultId"));

                    b.Property<float?>("AverageTimeOnTask")
                        .HasColumnType("real");

                    b.Property<int?>("Errors")
                        .HasColumnType("int");

                    b.Property<string>("ScrollDepthRate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("TaskSucessRate")
                        .HasColumnType("real");

                    b.Property<int?>("TimeElapsed")
                        .HasColumnType("int");

                    b.Property<int?>("UserClicks")
                        .HasColumnType("int");

                    b.Property<string>("VisitedSites")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ResultId");

                    b.ToTable("TestResults");
                });
#pragma warning restore 612, 618
        }
    }
}
