using Application;
using Infrastructure.Configurations.ConfigureService;
using MediatR;

namespace Web.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddServices(builder.Configuration);

        builder.Services.AddMediatR(typeof(MediatREntryPoint).Assembly);

        var app = builder.Build();

        app.MapControllers();

        app.UseAuthentication();
        
        app.UseAuthorization();

        app.Run();
    }
}