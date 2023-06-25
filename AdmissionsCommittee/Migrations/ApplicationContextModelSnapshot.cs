﻿// <auto-generated />
using System;
using AdmissionsCommittee.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdmissionsCommittee.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AdmissionsCommittee.Models.Certificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("AvarageScore")
                        .HasColumnType("double precision");

                    b.Property<int>("EnrolleeId")
                        .HasColumnType("integer");

                    b.Property<bool>("Original")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.Citizenship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Citizenship");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.Disability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Document")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Disability");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("District");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalEducation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("After11School")
                        .HasColumnType("boolean");

                    b.Property<bool>("After9School")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.Enrollee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int?>("CertificateId")
                        .HasColumnType("integer");

                    b.Property<int>("CertificateId1")
                        .HasColumnType("integer");

                    b.Property<int?>("CitizenshipId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<int?>("DisabilityId")
                        .HasColumnType("integer");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("integer");

                    b.Property<int?>("EducationId")
                        .HasColumnType("integer");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBudget")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEnlisted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.Property<int?>("PlaceOfResidenceId")
                        .HasColumnType("integer");

                    b.Property<string>("Snils")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SpecialityId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("WardId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("YearOfAdmission")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CertificateId1");

                    b.HasIndex("CitizenshipId");

                    b.HasIndex("DisabilityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("EducationId");

                    b.HasIndex("PlaceOfResidenceId");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("WardId");

                    b.ToTable("Enrollee");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.PlaceOfResidence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PlaceOfResidence");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Speciality");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.Ward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Document")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Ward");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.Enrollee", b =>
                {
                    b.HasOne("AdmissionsCommittee.Models.Certificate", "Certificate")
                        .WithMany()
                        .HasForeignKey("CertificateId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdmissionsCommittee.Models.Citizenship", "Citizenship")
                        .WithMany("Enrollees")
                        .HasForeignKey("CitizenshipId");

                    b.HasOne("AdmissionsCommittee.Models.Disability", "Disability")
                        .WithMany()
                        .HasForeignKey("DisabilityId");

                    b.HasOne("AdmissionsCommittee.Models.District", "District")
                        .WithMany("Enrollees")
                        .HasForeignKey("DistrictId");

                    b.HasOne("AdmissionsCommittee.Models.Education", "Education")
                        .WithMany()
                        .HasForeignKey("EducationId");

                    b.HasOne("AdmissionsCommittee.Models.PlaceOfResidence", "PlaceOfResidence")
                        .WithMany()
                        .HasForeignKey("PlaceOfResidenceId");

                    b.HasOne("AdmissionsCommittee.Models.Speciality", "Speciality")
                        .WithMany("Enrollees")
                        .HasForeignKey("SpecialityId");

                    b.HasOne("AdmissionsCommittee.Models.Ward", "Ward")
                        .WithMany()
                        .HasForeignKey("WardId");

                    b.Navigation("Certificate");

                    b.Navigation("Citizenship");

                    b.Navigation("Disability");

                    b.Navigation("District");

                    b.Navigation("Education");

                    b.Navigation("PlaceOfResidence");

                    b.Navigation("Speciality");

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.Citizenship", b =>
                {
                    b.Navigation("Enrollees");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.District", b =>
                {
                    b.Navigation("Enrollees");
                });

            modelBuilder.Entity("AdmissionsCommittee.Models.Speciality", b =>
                {
                    b.Navigation("Enrollees");
                });
#pragma warning restore 612, 618
        }
    }
}
