namespace Garnet.Common.Application.MessageBus;

public interface IMessageBus
{
    Task Publish<TMessage>(TMessage message);
}