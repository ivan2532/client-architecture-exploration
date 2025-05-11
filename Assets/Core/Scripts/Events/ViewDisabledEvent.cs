using Core.Infrastructure;

namespace Core.Events
{
    public record ViewDisabledEvent<TView>(TView View) : IEvent;
}