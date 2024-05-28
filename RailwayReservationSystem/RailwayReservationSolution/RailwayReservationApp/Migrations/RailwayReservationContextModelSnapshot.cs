﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RailwayReservationApp.Contexts;

#nullable disable

namespace RailwayReservationApp.Migrations
{
    [DbContext(typeof(RailwayReservationContext))]
    partial class RailwayReservationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RailwayReservationApp.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Disability")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"), 1L, 1);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Refund", b =>
                {
                    b.Property<int>("RefundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefundId"), 1L, 1);

                    b.Property<float>("RefundAmount")
                        .HasColumnType("real");

                    b.Property<string>("RefundDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservationCancelId")
                        .HasColumnType("int");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RefundId");

                    b.HasIndex("ReservationId");

                    b.HasIndex("UserId");

                    b.ToTable("Refunds");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"), 1L, 1);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<string>("EndingPoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StartingPoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TrainDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrainId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("TrainId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.ReservationCancel", b =>
                {
                    b.Property<int>("ReservationCancelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationCancelId"), 1L, 1);

                    b.Property<float>("RefundAmount")
                        .HasColumnType("real");

                    b.Property<string>("ReservationCancelReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationCancelId");

                    b.HasIndex("ReservationId");

                    b.HasIndex("UserId");

                    b.ToTable("ReservationCancels");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Rewards", b =>
                {
                    b.Property<int>("RewardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RewardId"), 1L, 1);

                    b.Property<float>("RewardPoints")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RewardId");

                    b.HasIndex("UserId");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeatId"), 1L, 1);

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.HasKey("SeatId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Station", b =>
                {
                    b.Property<int>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StationId"), 1L, 1);

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationPincode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StationId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackId"), 1L, 1);

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.Property<string>("TrackEndingPoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrackNumber")
                        .HasColumnType("int");

                    b.Property<string>("TrackStartingPoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrackId");

                    b.HasIndex("StationId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.TrackReservation", b =>
                {
                    b.Property<int>("TrackReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackReservationId"), 1L, 1);

                    b.Property<string>("ReservationStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TrackOccupiedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TrackOccupiedEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TrackOccupiedStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TrackOccupiedStartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("TrackReservationId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackReservations");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Train", b =>
                {
                    b.Property<int>("TrainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrainId"), 1L, 1);

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndingPoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PricePerKM")
                        .HasColumnType("real");

                    b.Property<string>("StartingPoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.Property<DateTime>("TrainEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrainName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrainNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("TrainStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrainStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrainId");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.TrainClass", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassId"), 1L, 1);

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassPrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EndingSeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("StartingSeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("TrainId")
                        .HasColumnType("int");

                    b.HasKey("ClassId");

                    b.HasIndex("TrainId");

                    b.ToTable("TrainClasses");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.TrainRoutes", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteId"), 1L, 1);

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<float>("KilometerDistance")
                        .HasColumnType("real");

                    b.Property<DateTime>("RouteEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RouteStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.Property<int>("StopNumber")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<int>("TrackNumber")
                        .HasColumnType("int");

                    b.Property<int>("TrainId")
                        .HasColumnType("int");

                    b.HasKey("RouteId");

                    b.HasIndex("StationId");

                    b.HasIndex("TrainId");

                    b.ToTable("TrainRoutes");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.UserDetails", b =>
                {
                    b.Property<int>("UserDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserDetailsId"), 1L, 1);

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordHashKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserDetailsId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Disability")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Payment", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Reservation", "Reservation")
                        .WithMany("Payments")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Refund", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.ReservationCancel", "ReservationCancel")
                        .WithMany()
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RailwayReservationApp.Models.Users", "Users")
                        .WithMany("Refunds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ReservationCancel");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Reservation", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Train", "Train")
                        .WithMany("TrainReservations")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RailwayReservationApp.Models.Users", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Train");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.ReservationCancel", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Reservation", "Reservation")
                        .WithMany("ReservationCancels")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RailwayReservationApp.Models.Users", "User")
                        .WithMany("ReservationCancels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Rewards", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Seat", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Reservation", "Reservation")
                        .WithMany("Seats")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Track", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Station", "Station")
                        .WithMany("Tracks")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Station");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.TrackReservation", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Track", "Track")
                        .WithMany("TrackReservations")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Track");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.TrainClass", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Train", "Train")
                        .WithMany("TrainClasses")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Train");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.TrainRoutes", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RailwayReservationApp.Models.Train", "Train")
                        .WithMany("TrainRoutes")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Station");

                    b.Navigation("Train");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.UserDetails", b =>
                {
                    b.HasOne("RailwayReservationApp.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Reservation", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("ReservationCancels");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Station", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Track", b =>
                {
                    b.Navigation("TrackReservations");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Train", b =>
                {
                    b.Navigation("TrainClasses");

                    b.Navigation("TrainReservations");

                    b.Navigation("TrainRoutes");
                });

            modelBuilder.Entity("RailwayReservationApp.Models.Users", b =>
                {
                    b.Navigation("Refunds");

                    b.Navigation("ReservationCancels");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
