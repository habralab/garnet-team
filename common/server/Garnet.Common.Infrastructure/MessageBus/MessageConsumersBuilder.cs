using Garnet.Common.Application.MessageBus;

namespace Garnet.Common.Infrastructure.MessageBus;

public class MessageConsumersBuilder
{
    private readonly List<(Type MessageType, Type? ConsumerType)> _consumersList = new();
    
    public MessageConsumersBuilder RegisterMessage<TMessage>()
    {
        _consumersList.Add((MessageType: typeof(TMessage), ConsumerType: null));
        return this;
    }
    
    public MessageConsumersBuilder RegisterConsumer<TConsumer, TMessage>() where TConsumer : IMessageBusConsumer<TMessage>
    {
        _consumersList.Add((MessageType: typeof(TMessage), ConsumerType: typeof(TConsumer)));
        return this;
    }

    public List<(Type MessageType, Type? ConsumerType)> Build()
    {
        return _consumersList;
    }
}