using Garnet.Common.Application.S3;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Garnet.Common.Infrastructure.S3;

public static class Startup
{
    public static IServiceCollection AddGarnetPublicStorage(
        this IServiceCollection services
    )
    {
        const string s3EndpointEnv = "S3_ENDPOINT";
        const string s3BucketEnv = "S3_BUCKET";
        const string s3AccessKeyEnv = "S3_ACCESS_KEY";
        const string s3SecretKeyEnv = "S3_SECRET_KEY";

        services.AddMinio(o =>
            {
                o.WithEndpoint(Environment.GetEnvironmentVariable(s3EndpointEnv));
                o.WithCredentials(Environment.GetEnvironmentVariable(s3AccessKeyEnv),
                    Environment.GetEnvironmentVariable(s3SecretKeyEnv));
                o.Build();
            },
            ServiceLifetime.Scoped
        );

        services.AddScoped<IRemoteFileStorage>(o =>
            new TimewebS3Storage(
                Environment.GetEnvironmentVariable(s3BucketEnv)
                ?? throw new Exception($"No {s3BucketEnv} environment variable was provided."),
                o.GetRequiredService<IMinioClient>()
            )
        );
        
        return services;
    }
}