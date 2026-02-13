using FluentValidation;
using LibraryService.BL;
using LibraryService.DL;
using LibraryService.Host.Healthchecks;
using LibraryService.Host.Validators;
using Mapster;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace LibraryService.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            builder.Services
                .AddDataLayer(builder.Configuration)
                .AddBusinessLayer();

            builder.Services.AddMapster();

            builder.Services.AddValidatorsFromAssemblyContaining<AddBookRequestValidator>();

            builder.Services.AddControllers();


            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Library Service", Version = "v1" });
            });

            builder.Host.UseSerilog();

            builder.Services
                .AddHealthChecks()
                .AddCheck<MongoPingHealthcheck>("mongo");

            var app = builder.Build();

            app.MapHealthChecks("/healthz");


            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "LibraryService V1");
            });

            app.UseSwagger();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
