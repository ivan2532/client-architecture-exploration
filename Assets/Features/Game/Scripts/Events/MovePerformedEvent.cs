using Core.Infrastructure;
using Features.Game.Domain;
using Features.Game.Domain.Model;

namespace Features.Game.Events
{
    public record MovePerformedEvent(MoveInput NormalizedInput) : IEvent;
}