using Core.EventSystem;
using Features.Game.View.Model;

namespace Features.Game.Events
{
    public record LookPerformedEvent(LookInputDelta InputDelta) : IEvent;
}