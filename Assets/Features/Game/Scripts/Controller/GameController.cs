using System;
using Core.Controller;
using Core.Infrastructure;
using Features.Game.Configuration;
using Features.Game.Events;
using Features.Game.Mappers;
using Features.Game.Model;
using Features.Game.View;
using JetBrains.Annotations;

namespace Features.Game.Controller
{
    [UsedImplicitly]
    public class GameController : ControllerBase<GameView>, IDisposable
    {
        private readonly GameView _view;
        private readonly GameConfiguration _configuration;
        private readonly GameModel _game;

        public GameController(GameView view, GameConfiguration configuration) : base(view)
        {
            _view = view;
            _configuration = configuration;

            var drone = new DroneModel(configuration.Drone, view.DronePitch, view.DroneYaw);
            _game = new GameModel(drone);

            EventBus.Subscribe<LookPerformedEvent>(OnLookPerformed);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<LookPerformedEvent>(OnLookPerformed);
        }

        private void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _game.OnLookPerformed(lookPerformedEvent);
            _view.UpdateViewModel(GameModelToViewModelMapper.Map(_game));
        }
    }
}