using Microsoft.EntityFrameworkCore;
using RailwayReservationApp.Contexts;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Repositories;
using RailwayReservationApp.Repositories.ReservationRequest;
using RailwayReservationApp.Repositories.StationRequest;
using RailwayReservationApp.Repositories.TrackRequest;
using RailwayReservationApp.Repositories.TrainRequest;
using RailwayReservationApp.Services;

namespace RailwayReservationApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Contexts
            builder.Services.AddDbContext<RailwayReservationContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
                );
            #endregion

            #region Repositories

            builder.Services.AddScoped<IRepository<int, Users>, UserRepository>();
            builder.Services.AddScoped<IRepository<int, Admin>, AdminRepository>();
            builder.Services.AddScoped<IRepository<int, Payment>, PaymentRepository>();
            builder.Services.AddScoped<IRepository<int, Reservation>, ReservationRepository>();
            builder.Services.AddScoped<IRepository<int, ReservationCancel>, ReservationCancelRepository>();
            builder.Services.AddScoped<IRepository<int, Rewards>, RewardRepository>();
            builder.Services.AddScoped<IRepository<int, Seat>, SeatRepository>();
            builder.Services.AddScoped<IRepository<int, Station>, StationRepository>();
            builder.Services.AddScoped<IRepository<int, Track>, TrackRepository>();
            builder.Services.AddScoped<IRepository<int, TrainClass>, TrainClassRepository>();
            builder.Services.AddScoped<IRepository<int, Train>, TrainRepository>();
            builder.Services.AddScoped<IRepository<int, TrainRoutes>, TrainRouteRepository>();
            builder.Services.AddScoped<IRepository<int, UserDetails>, UserDetailRepository>();
            builder.Services.AddScoped<IRepository<int, TrackReservation>, TrackReservationRepository>();
            // Station Request Repository(To retrieve Tracks of a station)
            builder.Services.AddScoped<StationRequestRepository>();
            // Train Request Repository(To retrieve Classes of a  Train)
            builder.Services.AddScoped<TrainRequestforClassesRepository>();
            // Track Request Repository(To retrieve Reservations of a Track)
            builder.Services.AddScoped<TrackRequestforReservationRepository>();
            // Train Request Repository(To retrieve Routes of a Train)
            builder.Services.AddScoped<TrainRequestforTrainRoutesRepository>();
            // Reservation Request Repository(To retrieve Seats of a Reservation)
            builder.Services.AddScoped<ReservationRequestforSeatsRepository>();
            // Train Request Repository(To retrieve Reservations of a Train)
            builder.Services.AddScoped<TrainRequestforReservationsRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IAdminService, AdminServices>();
            builder.Services.AddScoped<IUserService, UserServices>();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
