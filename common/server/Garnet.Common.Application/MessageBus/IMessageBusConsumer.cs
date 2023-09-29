namespace Garnet.Common.Application.MessageBus;

public interface IMessageBusConsumer<in TMessage>
{
    Task Consume(TMessage message);
}