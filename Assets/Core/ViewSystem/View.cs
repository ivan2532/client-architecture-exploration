using UnityEngine;

namespace Core.ViewSystem
{
    public abstract class View : MonoBehaviour
    {
        protected virtual void Awake()
        {
            RaiseViewCreatedEvent();
        }

        protected abstract void RaiseViewCreatedEvent();
    }
    
    public abstract class View<TViewModel> : View
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