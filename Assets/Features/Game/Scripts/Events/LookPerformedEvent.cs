using Core.Infrastructure;
using Features.Game.Domain;

namespace Features.Game.Events
{
    public record LookPerformedEvent(LookInputDelta InputDelta) : IEvent;
}