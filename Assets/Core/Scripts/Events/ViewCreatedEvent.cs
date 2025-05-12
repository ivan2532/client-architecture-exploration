using Core.Infrastructure;

namespace Core.Events
{
    public record ViewCreatedEvent<TView>(TView View) : IEvent;
}