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
    public class DroneController : Controller<DroneView>, IDisposable
    {
        private readonly DroneView _view;
        private readonly Drone _model;

        public DroneController(DroneView view, DroneConfiguration configuration) : base(view)
        {
            _view = view;
            _model = new Drone(configuration, view.Pitch, view.Yaw, view.OffsetFromMainCharacter);

            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Subscribe<DroneUpdateEvent>(OnUpdate);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Unsubscribe<DroneUpdateEvent>(OnUpdate);
        }

        private void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _model.OnLookPerformed(lookPerformedEvent);
            UpdateViewModel();
        }

        private void OnUpdate(DroneUpdateEvent updateEvent)
        {
            _model.OnUpdate(updateEvent);
            UpdateViewModel();
        }

        private void UpdateViewModel()
        {
            _view.UpdateViewModel(_model.CreateViewModel());
        }
    }
}