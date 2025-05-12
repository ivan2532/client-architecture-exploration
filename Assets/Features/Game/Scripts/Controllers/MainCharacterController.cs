using System;
using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.Game.Configuration;
using Features.Game.Events;
using Features.Game.Models;
using Features.Game.Views;
using JetBrains.Annotations;

namespace Features.Game.Controllers
{
    [UsedImplicitly]
    public class MainCharacterController : Controller<MainCharacterView>, IDisposable
    {
        private readonly MainCharacterView _view;
        private readonly MainCharacter _model;

        public MainCharacterController(
            MainCharacterView view,
            MainCharacterConfiguration configuration
        ) : base(view)
        {
            _view = view;
            _model = new MainCharacter(configuration);

            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Subscribe<MoveCancelledEvent>(OnMoveCancelled);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Unsubscribe<MoveCancelledEvent>(OnMoveCancelled);
        }

        private void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _model.OnMovePerformed(movePerformedEvent);
            UpdateViewModel();
        }

        private void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            _model.OnMoveCancelled(moveCancelledEvent);
            UpdateViewModel();
        }

        private void UpdateViewModel()
        {
            _view.UpdateViewModel(_model.CreateViewModel());
        }
    }
}