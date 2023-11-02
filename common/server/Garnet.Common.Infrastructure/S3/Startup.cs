using Garnet.Common.Application.S3;
using Garnet.Common.Infrastructure.Support;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Garnet.Common.Infrastructure.S3;

public static class Startup
{
    public static IServiceCollection AddGarnetPublicStorage(
        this IServiceCollection services
    )
    {
        services.AddMinio(o =>
            {
                // Господи спаси и сохрани разработчиков библиотеки minio-dotnet
                // которые не сумели сделать на ПРОСТЕЙШЕМ функционале
                // корректную регистрацию scope-объектов
                // из-за чего их объект-клиент живет как signleton всегда и это поведение
                // невозможно переопределить. А единственное место, где можно отключить dispose
                // используемого внутри HttpClient - это вызов этого всратого метода ниже.
                // СПАСИБО, что дали хотя бы такую возможность!
                
                o.WithHttpClient(null); // <------ ВОТ ОН
                
                o.WithEndpoint(EnvironmentEx.GetRequiredEnvironmentVariable("S3_ENDPOINT"));
                o.WithCredentials(
                    EnvironmentEx.GetRequiredEnvironmentVariable("S3_ACCESS_KEY"),
                    EnvironmentEx.GetRequiredEnvironmentVariable("S3_SECRET_KEY")
                );
                o.Build();
            },
            ServiceLifetime.Scoped
        );

        services.AddScoped<IRemoteFileStorage>(o =>
            new TimewebS3Storage(
                EnvironmentEx.GetRequiredEnvironmentVariable("S3_BUCKET"),
                o.GetRequiredService<IMinioClient>()
            )
        );
        
        return services;
    }
}