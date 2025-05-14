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
        public delegate void ViewModelUpdatedDelegate(TViewModel viewModel);

        public event ViewModelUpdatedDelegate ViewModelUpdated;

        protected TViewModel ViewModel { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();

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