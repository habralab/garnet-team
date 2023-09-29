using Garnet.Common.Application.MessageBus;

namespace Garnet.Common.Infrastructure.MessageBus;

public class SlimMessageBus : IMessageBus
{
    private readonly global::SlimMessageBus.IMessageBus _messageBus;

    public SlimMessageBus(global::SlimMessageBus.IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }
    
    public async Task Publish<TMessage>(TMessage message)
    {
        await _messageBus.Publish(message);
    }
}