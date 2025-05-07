using Core.Infrastructure;
using Features.Game.Model;

namespace Features.Game.Events
{
    public record LookPerformedEvent(LookInputDelta InputDelta) : IEvent;
}