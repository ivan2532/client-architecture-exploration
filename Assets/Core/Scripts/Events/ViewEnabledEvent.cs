using Core.Infrastructure;
using Core.View;

namespace Core.Events
{
    public record ViewEnabledEvent(ViewBase View) : IEvent;
}