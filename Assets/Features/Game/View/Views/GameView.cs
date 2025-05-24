using Core.EventSystem;
using Features.Game.Events;
using Features.Game.View.Model;

namespace Features.Game.View.Views
{
    public abstract class GameView : Core.ViewSystem.View
    {
        protected override void RaiseViewCreatedEvent()
        {
            EventBus.Raise(new GameViewCreatedEvent(this));
        }
    }

    public abstract class GameView<TViewModel> : GameView where TViewModel : IGameViewModel
    {
        public delegate void ViewModelUpdatedDelegate(TViewModel viewModel);

        public event ViewModelUpdatedDelegate ViewModelUpdated;

        protected TViewModel ViewModel { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            var initialViewModel = Initialize();
            UpdateViewModel(initialViewModel);
        }

        protected abstract TViewModel Initialize();

        public void UpdateViewModel(TViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModelUpdated?.Invoke(viewModel);
        }
    }
}