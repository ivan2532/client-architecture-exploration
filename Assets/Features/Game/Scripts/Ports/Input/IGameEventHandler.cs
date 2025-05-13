namespace Features.Game.Ports.Input
{
    public interface IGameEventHandler
    {
        public void Enable();

        public void Disable();
    }
}