using System.Security.Claims;
using DotNetEnv.Configuration;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.Identity.SecretKey;
using Garnet.Common.Infrastructure.Migrations;
using Garnet.User;
using Garnet.Project;

namespace Garnet;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddDotNetEnvMulti(new [] {"../../../.env", "../../../.env.stage"});
        }

        builder.Configuration.AddEnvironmentVariables();

        builder.WebHost.UseUrls("http://*:3000");

        builder.Services
            .AddAuthentication("SecretKey")
            .AddScheme<SecretKeyAuthenticationSchemeOptions, SecretKeyAuthenticationHandler>("SecretKey", null);
        
        builder.Services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType(o => o.Name("Query"))
            .AddMutationType(o => o.Name("Mutation"))
            .AddMutationConventions(applyToAllMutations: true)
            .AddGarnetUsers()
            .AddGarnetProjects();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapGraphQL("/api/graphql");

        using (var scope = app.Services.CreateScope())
        {
            await scope.ServiceProvider.ExecuteRepeatableMigrations(CancellationToken.None);
        }
        
        await app.RunAsync();
    }
}