﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestToolApi.Data;

#nullable disable

namespace TestToolApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250319164305_firstMigration")]
    partial class firstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.14");

            modelBuilder.Entity("DataModel.Projects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int>("PassPercent")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RmsNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestCaseNum")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DataModel.TestCases", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChangeDescription")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("TestCaseDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("TestCaseId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TestCaseName")
                        .HasColumnType("TEXT");

                    b.Property<string>("TestCasePrecondition")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestSuiteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TestSuiteId");

                    b.ToTable("TestCases");
                });

            modelBuilder.Entity("DataModel.TestScripts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<double>("ScriptCompletedPercent")
                        .HasColumnType("REAL");

                    b.Property<int?>("ScriptEnvironment")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ScriptExpectedResult")
                        .HasColumnType("TEXT");

                    b.Property<string>("ScriptReferToTransaction")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ScriptResult")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ScriptRowMessage")
                        .HasColumnType("TEXT");

                    b.Property<string>("ScriptStepDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("ScriptStepNum")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestCaseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TestCaseId");

                    b.ToTable("TestScripts");
                });

            modelBuilder.Entity("DataModel.TestSuites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChangeDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cr3atReference")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JiraReference")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RequirementDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequirementName")
                        .HasColumnType("TEXT");

                    b.Property<int>("SuiteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("TestSuites");
                });

            modelBuilder.Entity("DataModel.TestCases", b =>
                {
                    b.HasOne("DataModel.TestSuites", "TestSuite")
                        .WithMany()
                        .HasForeignKey("TestSuiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestSuite");
                });

            modelBuilder.Entity("DataModel.TestScripts", b =>
                {
                    b.HasOne("DataModel.TestCases", "TestCase")
                        .WithMany()
                        .HasForeignKey("TestCaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestCase");
                });

            modelBuilder.Entity("DataModel.TestSuites", b =>
                {
                    b.HasOne("DataModel.Projects", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });
#pragma warning restore 612, 618
        }
    }
}
