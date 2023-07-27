using MassTransit;
using MessageQueue.Interfaces;
using MessageQueue.Models;

namespace MessageQueue;


public class MassTransitProducer : IMassTransitProducer
{
    private readonly IBus _bus;

    public MassTransitProducer(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishNewCompany(string id)
    {
        await _bus.Publish<NewCompany>(new NewCompany
        {
            CompanyId = id
        });
    }
}