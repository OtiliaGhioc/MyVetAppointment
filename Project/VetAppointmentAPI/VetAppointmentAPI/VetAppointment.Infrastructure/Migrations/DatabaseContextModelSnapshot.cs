﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VetAppointment.Infrastructure.Context;

#nullable disable

namespace VetAppointment.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("VetAppointment.Domain.Entities.Appointment", b =>
                {
                    b.Property<Guid>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointeeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("isExpired")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppointmentId");

                    b.HasIndex("AppointeeId");

                    b.HasIndex("AppointerId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.BillingEntry", b =>
                {
                    b.Property<Guid>("BillingEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IssuerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PrescriptionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("BillingEntryId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("IssuerId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("BillingEntries");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.Drug", b =>
                {
                    b.Property<Guid>("DrugId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DrugId");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.DrugStock", b =>
                {
                    b.Property<Guid>("DrugStockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("TEXT");

                    b.HasKey("DrugStockId");

                    b.HasIndex("TypeId");

                    b.ToTable("DrugStocks");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.MedicalHistoryEntry", b =>
                {
                    b.Property<Guid>("MedicalHistoryEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PrescriptionId")
                        .HasColumnType("TEXT");

                    b.HasKey("MedicalHistoryEntryId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("MedicalEntries");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.Office", b =>
                {
                    b.Property<Guid>("OfficeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("OfficeId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.Prescription", b =>
                {
                    b.Property<Guid>("PrescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PrescriptionId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.PrescriptionDrug", b =>
                {
                    b.Property<Guid>("PrescriptionDrugId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PrescriptionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("StockId")
                        .HasColumnType("TEXT");

                    b.HasKey("PrescriptionDrugId");

                    b.HasIndex("PrescriptionId");

                    b.HasIndex("StockId");

                    b.ToTable("PrescriptionDrugs");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasOffice")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserOfficeOfficeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("UserOfficeOfficeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.Appointment", b =>
                {
                    b.HasOne("VetAppointment.Domain.Entities.User", "Appointee")
                        .WithMany()
                        .HasForeignKey("AppointeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetAppointment.Domain.Entities.User", "Appointer")
                        .WithMany()
                        .HasForeignKey("AppointerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointee");

                    b.Navigation("Appointer");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.BillingEntry", b =>
                {
                    b.HasOne("VetAppointment.Domain.Entities.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetAppointment.Domain.Entities.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetAppointment.Domain.Entities.User", "Issuer")
                        .WithMany()
                        .HasForeignKey("IssuerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetAppointment.Domain.Entities.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Customer");

                    b.Navigation("Issuer");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.DrugStock", b =>
                {
                    b.HasOne("VetAppointment.Domain.Entities.Drug", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.MedicalHistoryEntry", b =>
                {
                    b.HasOne("VetAppointment.Domain.Entities.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetAppointment.Domain.Entities.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.PrescriptionDrug", b =>
                {
                    b.HasOne("VetAppointment.Domain.Entities.Prescription", null)
                        .WithMany("Drugs")
                        .HasForeignKey("PrescriptionId");

                    b.HasOne("VetAppointment.Domain.Entities.DrugStock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.User", b =>
                {
                    b.HasOne("VetAppointment.Domain.Entities.Office", "UserOffice")
                        .WithMany()
                        .HasForeignKey("UserOfficeOfficeId");

                    b.Navigation("UserOffice");
                });

            modelBuilder.Entity("VetAppointment.Domain.Entities.Prescription", b =>
                {
                    b.Navigation("Drugs");
                });
#pragma warning restore 612, 618
        }
    }
}
