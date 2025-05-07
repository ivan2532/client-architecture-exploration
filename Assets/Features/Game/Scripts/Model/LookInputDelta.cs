using Core.Infrastructure;

namespace Features.Game.Model
{
    public record LookInputDelta(float X, float Y) : IEvent;
}