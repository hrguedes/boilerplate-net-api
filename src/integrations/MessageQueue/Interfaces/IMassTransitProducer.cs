namespace MessageQueue.Interfaces;

public interface IMassTransitProducer
{
    Task PublishNewCompany(string id);
}