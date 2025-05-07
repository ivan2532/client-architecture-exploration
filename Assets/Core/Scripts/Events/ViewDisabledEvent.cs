using Core.Infrastructure;
using Core.View;

namespace Core.Events
{
    public record ViewDisabledEvent(ViewBase View) : IEvent;
}