using DotNetEnv.Configuration;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.Team;
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

        builder.Services.AddKratosAuth(); // Uncomment when Kratos will be ready to handle authorization
        builder.Services.AddSecretKeyAuth();
        
        builder.Services
            .AddHttpContextAccessor()
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType(o => o.Name("Query"))
            .AddMutationType(o => o.Name("Mutation"))
            .AddUploadType()
            .AddMutationConventions(applyToAllMutations: true)
            .AddGarnetUsers()
            .AddGarnetTeams()
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
        app.MapGraphQLHttp("/api/graphql").RequireAuthorization();
        app.MapGraphQL("/api/sandbox");

        using (var scope = app.Services.CreateScope())
        {
            await scope.ServiceProvider.ExecuteRepeatableMigrations(CancellationToken.None);
        }
        
        await app.RunAsync();
    }
}