using Core.Events;
using UnityEngine;

namespace Core.Infrastructure
{
    public abstract class View<TView> : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            EventBus.Raise(new ViewEnabledEvent<TView>(GetComponent<TView>()));
        }

        protected virtual void OnDisable()
        {
            EventBus.Raise(new ViewDisabledEvent<TView>(GetComponent<TView>()));
        }
    }

    public abstract class View<TView, TViewModel> : View<TView> where TViewModel : IViewModel
    {
        public TViewModel ViewModel
        {
            get
            {
                InitializeViewModelIfNeeded();
                return _viewModel;
            }

            private set => _viewModel = value;
        }

        private bool _viewModelInitialized;
        private TViewModel _viewModel;

        protected override void OnEnable()
        {
            base.OnEnable();
            InitializeViewModelIfNeeded();
        }

        public void UpdateViewModel(TViewModel viewModel)
        {
            ViewModel = viewModel;
            OnViewModelUpdated();
        }

        protected abstract TViewModel CreateInitialViewModel();

        protected virtual void OnViewModelUpdated()
        {
            _viewModelInitialized = true;
        }

        private void InitializeViewModelIfNeeded()
        {
            if (!_viewModelInitialized)
            {
                var initialViewModel = CreateInitialViewModel();
                UpdateViewModel(initialViewModel);
            }
        }
    }
}