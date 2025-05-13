using UnityEngine;

namespace Core.Infrastructure
{
    public abstract class View<TViewCreatedEventFactory> : MonoBehaviour
        where TViewCreatedEventFactory : IViewCreatedEventFactory, new()
    {
        protected virtual void Awake()
        {
            var viewCreatedEvent = new TViewCreatedEventFactory().Create(this);
            EventBus.Raise(viewCreatedEvent.GetType(), viewCreatedEvent);
        }
    }

    public abstract class View<TViewCreatedEventFactory, TViewModel> : View<TViewCreatedEventFactory>
        where TViewCreatedEventFactory : IViewCreatedEventFactory, new()
        where TViewModel : IViewModel
    {
        protected TViewModel ViewModel
        {
            get
            {
                InitializeIfNeeded();
                return _viewModel;
            }

            private set => _viewModel = value;
        }

        private bool _initialized;
        private TViewModel _viewModel;

        protected override void Awake()
        {
            base.Awake();
            InitializeIfNeeded();
        }

        public void UpdateViewModel(TViewModel viewModel)
        {
            ViewModel = viewModel;
            OnViewModelUpdated();
        }

        protected abstract TViewModel Initialize();

        protected virtual void OnViewModelUpdated()
        {
            _initialized = true;
        }

        private void InitializeIfNeeded()
        {
            if (!_initialized)
            {
                var initialViewModel = Initialize();
                UpdateViewModel(initialViewModel);
            }
        }
    }
}