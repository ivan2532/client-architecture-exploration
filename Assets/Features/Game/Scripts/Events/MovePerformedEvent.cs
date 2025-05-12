using Core.Infrastructure;
using Features.Game.Models;

namespace Features.Game.Events
{
    public record MovePerformedEvent(MoveInput NormalizedInput) : IEvent;
}