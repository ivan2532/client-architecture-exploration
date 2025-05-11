using Core.Infrastructure;
using Core.Infrastructure.ViewController;

namespace Core.Events
{
    public record ViewDisabledEvent(View View) : IEvent;
}