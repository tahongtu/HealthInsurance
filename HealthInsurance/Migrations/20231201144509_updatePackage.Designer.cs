﻿// <auto-generated />
using HealthInsurance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthInsurance.Migrations
{
    [DbContext(typeof(HealthInsuranceContext))]
    [Migration("20231201144509_updatePackage")]
    partial class updatePackage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HealthInsurance.Models.BenefitDetail", b =>
                {
                    b.Property<int>("BenefitDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BenefitDetailId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BenefitDetailId");

                    b.HasIndex("ProductId");

                    b.ToTable("BenefitDetail");
                });

            modelBuilder.Entity("HealthInsurance.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryDesription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("HealthInsurance.Models.DiseaseList", b =>
                {
                    b.Property<int>("DiseaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiseaseId"), 1L, 1);

                    b.Property<string>("DiseaseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lv1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lv2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lv3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("DiseaseId");

                    b.HasIndex("ProductId");

                    b.ToTable("DiseaseList");
                });

            modelBuilder.Entity("HealthInsurance.Models.InsuranceProducts", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("InsuranceProducts");
                });

            modelBuilder.Entity("HealthInsurance.Models.OutstandingBenefit", b =>
                {
                    b.Property<int>("ObId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ObId"), 1L, 1);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OdDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OdName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ObId");

                    b.HasIndex("ProductId");

                    b.ToTable("OutstandingBenefit");
                });

            modelBuilder.Entity("HealthInsurance.Models.PackageInsurance", b =>
                {
                    b.Property<int>("PackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageId"), 1L, 1);

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Limit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("PackageId");

                    b.HasIndex("ProductId");

                    b.ToTable("PackageInsurance");
                });

            modelBuilder.Entity("HealthInsurance.Models.ParticipationInformation", b =>
                {
                    b.Property<int>("ParticipationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParticipationId"), 1L, 1);

                    b.Property<string>("File")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PIDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PIName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ParticipationId");

                    b.HasIndex("ProductId");

                    b.ToTable("ParticipationInformation");
                });

            modelBuilder.Entity("HealthInsurance.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HealthInsurance.Models.BenefitDetail", b =>
                {
                    b.HasOne("HealthInsurance.Models.InsuranceProducts", "InsuranceProducts")
                        .WithMany("BenefitDetail")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceProducts");
                });

            modelBuilder.Entity("HealthInsurance.Models.DiseaseList", b =>
                {
                    b.HasOne("HealthInsurance.Models.InsuranceProducts", "InsuranceProducts")
                        .WithMany("DiseaseList")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceProducts");
                });

            modelBuilder.Entity("HealthInsurance.Models.InsuranceProducts", b =>
                {
                    b.HasOne("HealthInsurance.Models.Category", "Category")
                        .WithMany("InsuranceProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("HealthInsurance.Models.OutstandingBenefit", b =>
                {
                    b.HasOne("HealthInsurance.Models.InsuranceProducts", "InsuranceProducts")
                        .WithMany("OutstandingBenefit")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceProducts");
                });

            modelBuilder.Entity("HealthInsurance.Models.PackageInsurance", b =>
                {
                    b.HasOne("HealthInsurance.Models.InsuranceProducts", "InsuranceProducts")
                        .WithMany("PackageInsurances")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceProducts");
                });

            modelBuilder.Entity("HealthInsurance.Models.ParticipationInformation", b =>
                {
                    b.HasOne("HealthInsurance.Models.InsuranceProducts", "InsuranceProducts")
                        .WithMany("ParticipationInformation")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceProducts");
                });

            modelBuilder.Entity("HealthInsurance.Models.Category", b =>
                {
                    b.Navigation("InsuranceProducts");
                });

            modelBuilder.Entity("HealthInsurance.Models.InsuranceProducts", b =>
                {
                    b.Navigation("BenefitDetail");

                    b.Navigation("DiseaseList");

                    b.Navigation("OutstandingBenefit");

                    b.Navigation("PackageInsurances");

                    b.Navigation("ParticipationInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
