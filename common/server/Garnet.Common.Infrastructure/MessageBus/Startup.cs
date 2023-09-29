using Garnet.Common.Application.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using SlimMessageBus.Host;
using SlimMessageBus.Host.Memory;

namespace Garnet.Common.Infrastructure.MessageBus;

public static class Startup
{
    public static IServiceCollection AddGarnetMessageBus(
        this IServiceCollection services,
        string name,
        Action<MessageConsumersBuilder> configure)
    {
        services.AddSlimMessageBus(pb =>
        {
            pb.AddChildBus(name,
                b =>
                {
                    b.WithProviderMemory();
            
                    var builder = new MessageConsumersBuilder();
                    configure(builder);
                    var messagesAndConsumers = builder.Build();

                    foreach (var (messageType, consumerType) in messagesAndConsumers)
                    {
                        b.Produce(messageType,
                            o =>
                                o.DefaultPath(messageType.FullName));

                        if (consumerType is not null)
                        {
                            services.AddScoped(consumerType);
                            b.Consume(messageType,
                                o =>
                                    o.WithConsumer(consumerType, nameof(IMessageBusConsumer<object>.Consume))
                                        .Path(messageType.FullName)
                            );
                        }
                    }
                });
        });

        services.AddScoped<IMessageBus, SlimMessageBus>();

        return services;
    }
}