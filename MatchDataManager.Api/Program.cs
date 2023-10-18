using MatchDataManager.Api.Repositories.Impl;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MatchDataManager.Api
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.RegisterAppServices();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static WebApplicationBuilder RegisterAppServices(this WebApplicationBuilder appBuilder)
        {
            appBuilder.Services.AddSingleton<IDbClient, DbClient>(s => ActivatorUtilities.CreateInstance<DbClient>(s, AppStrings.LocalDbPath));
            appBuilder.Services.AddSingleton<ILocationsRepository, LocationsRepository>();
            appBuilder.Services.AddSingleton<ITeamsRepository, TeamsRepository>();

            return appBuilder;
        }
    }
}