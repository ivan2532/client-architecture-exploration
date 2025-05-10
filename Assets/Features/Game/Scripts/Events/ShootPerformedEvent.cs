using Core.Infrastructure;
using Features.Game.View;

namespace Features.Game.Events
{
    public record ShootPerformedEvent(RaycastShootResult RaycastShootResult) : IEvent;
}