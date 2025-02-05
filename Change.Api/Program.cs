using MediatR;
using Scalar.AspNetCore;

namespace Change.Api;

internal static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.Scan(selector => selector
            .FromAssemblies(
                Infrastructure.AssemblyReference.Assembly,
                Persistence.AssemblyReference.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

        builder.Services.AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options=>
            {
                options.WithTitle("Sample Coding")
                       .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
