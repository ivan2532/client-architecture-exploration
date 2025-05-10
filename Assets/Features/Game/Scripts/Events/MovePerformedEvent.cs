using Core.Infrastructure;
using Features.Game.Domain;

namespace Features.Game.Events
{
    public record MovePerformedEvent(MoveInput NormalizedInput) : IEvent;
}