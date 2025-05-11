using Core.Infrastructure;
using Features.Game.Domain;
using Features.Game.Views;

namespace Features.Game.Events
{
    public record LookPerformedEvent(LookInputDelta InputDelta) : IEvent;
}