using Core.Infrastructure;
using Core.Infrastructure.ViewController;

namespace Core.Events
{
    public record ViewEnabledEvent(View View) : IEvent;
}