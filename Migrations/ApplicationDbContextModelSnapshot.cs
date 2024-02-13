﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientInfoAPI_Assignments;

#nullable disable

namespace PatientInfoAPIAssignments.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PatientInfoAPI_Assignments.Entites.Disease", b =>
                {
                    b.Property<int>("DiseaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiseaseId"));

                    b.Property<string>("DiseaseName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("DiseaseId");

                    b.ToTable("Diseases", (string)null);
                });

            modelBuilder.Entity("PatientInfoAPI_Assignments.Entites.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors", (string)null);
                });

            modelBuilder.Entity("PatientInfoAPI_Assignments.Entites.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("PatientId");

                    b.ToTable("Patients", (string)null);
                });

            modelBuilder.Entity("PatientInfoAPI_Assignments.Entites.Visit", b =>
                {
                    b.Property<int>("VisitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisitId"));

                    b.Property<int>("ConsultingDoctorId")
                        .HasColumnType("int");

                    b.Property<int>("DiseaseId")
                        .HasMaxLength(255)
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VisitDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("VisitId");

                    b.HasIndex("ConsultingDoctorId");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("PatientId");

                    b.ToTable("Visits", (string)null);
                });

            modelBuilder.Entity("PatientInfoAPI_Assignments.Entites.Visit", b =>
                {
                    b.HasOne("PatientInfoAPI_Assignments.Entites.Doctor", "ConsultingDoctor")
                        .WithMany()
                        .HasForeignKey("ConsultingDoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PatientInfoAPI_Assignments.Entites.Disease", "DiseaseNavigation")
                        .WithMany()
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PatientInfoAPI_Assignments.Entites.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConsultingDoctor");

                    b.Navigation("DiseaseNavigation");

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
