using Core.Events;
using UnityEngine;

namespace Core.Infrastructure.ViewController
{
    public abstract class View : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            EventBus.Raise(new ViewEnabledEvent(this));
        }

        protected virtual void OnDisable()
        {
            EventBus.Raise(new ViewDisabledEvent(this));
        }
    }

    public abstract class View<TViewModel> : View where TViewModel : IViewModel
    {
        public TViewModel ViewModel
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

        protected override void OnEnable()
        {
            base.OnEnable();
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