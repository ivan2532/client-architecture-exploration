using Core.Infrastructure;

namespace Core.Events
{
    public record ViewEnabledEvent<TView>(TView View) : IEvent;
}