using Core.Events;
using Core.Infrastructure;
using Core.ViewModel;
using UnityEngine;

namespace Core.View
{
    public abstract class ViewBase : MonoBehaviour
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

    public abstract class ViewBase<TViewModel> : ViewBase where TViewModel : IViewModel
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