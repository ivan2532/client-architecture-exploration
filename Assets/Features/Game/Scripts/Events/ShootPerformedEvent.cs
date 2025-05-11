using Core.Infrastructure;
using Features.Game.Views;

namespace Features.Game.Events
{
    public record ShootPerformedEvent(RaycastShootResult RaycastShootResult) : IEvent;
}