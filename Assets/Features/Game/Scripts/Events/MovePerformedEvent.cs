using Core.Infrastructure;
using Features.Game.Model;

namespace Features.Game.Events
{
    public record MovePerformedEvent(MoveInput NormalizedInput) : IEvent;
}