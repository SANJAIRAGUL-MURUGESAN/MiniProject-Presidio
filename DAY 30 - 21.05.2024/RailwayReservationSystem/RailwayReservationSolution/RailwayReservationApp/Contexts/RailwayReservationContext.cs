using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Models;

namespace RailwayReservationApp.Contexts
{
    public class RailwayReservationContext : DbContext
    {
        public RailwayReservationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainClass> TrainClasses { get; set; }
        public DbSet<TrainRoutes> TrainRoutes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ReservationCancel> ReservationCancels { get; set; }
        public DbSet<Rewards> Rewards { get; set; }

        public DbSet<Refund> Refunds { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping Train and its Routes
            modelBuilder.Entity<TrainRoutes>()
              .HasOne(r => r.Train)
              .WithMany(e => e.TrainRoutes)
              .HasForeignKey(r => r.TrainId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Train and its Classes
            modelBuilder.Entity<TrainClass>()
              .HasOne(r => r.Train)
              .WithMany(e => e.TrainClasses)
              .HasForeignKey(r => r.TrainId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Reservation and corresponding Train
            modelBuilder.Entity<Reservation>()
              .HasOne(r => r.Train)
              .WithMany(e => e.TrainReservations)
              .HasForeignKey(r => r.TrainId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Reservation and Users
            modelBuilder.Entity<Reservation>()
              .HasOne(r => r.User)
              .WithMany(e => e.Reservations)
              .HasForeignKey(r => r.UserId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Payments and Reservations
            modelBuilder.Entity<Payment>()
              .HasOne(r => r.Reservation)
              .WithMany(e => e.Payments)
              .HasForeignKey(r => r.ReservationId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            // Mapping Reservation Cancels and Users
            modelBuilder.Entity<ReservationCancel>()
              .HasOne(r => r.User)
              .WithMany(e => e.ReservationCancels)
              .HasForeignKey(r => r.UserId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

            //modelBuilder.Entity<ReservationCancel>()
            //    .HasOne(r => r.Reservation)
            //    .WithMany(e => e.ReservationCancels)
            //    .HasForeignKey(r => r.ReservationId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .IsRequired();

            // Mapping Refunds and Users
            modelBuilder.Entity<Refund>()
             .HasOne(r => r.Users)
             .WithMany(e => e.Refunds)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired();

            // Mapping Reservation and Seats
            modelBuilder.Entity<Seat>()
             .HasOne(r => r.Reservation)
             .WithMany(e => e.Seats)
             .HasForeignKey(r => r.ReservationId)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired();

            // Mapping Track and Station
            modelBuilder.Entity<Track>()
             .HasOne(r => r.Station)
             .WithMany(e => e.Tracks)
             .HasForeignKey(r => r.StationId)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired();

        }
    }
}
