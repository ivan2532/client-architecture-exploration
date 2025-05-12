using Core.Events;
using UnityEngine;

namespace Core.Infrastructure
{
    public abstract class View<TView> : MonoBehaviour
    {
        protected virtual void Awake()
        {
            EventBus.Raise(new ViewCreatedEvent<TView>(GetComponent<TView>()));
        }
    }

    public abstract class View<TView, TViewModel> : View<TView> where TViewModel : IViewModel
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