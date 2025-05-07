using Core.Events;
using Core.Infrastructure;
using Core.ViewModel;
using UnityEngine;

namespace Core.View
{
    public abstract class ViewBase : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBus.Raise(new ViewEnabledEvent(this));
        }

        private void OnDisable()
        {
            EventBus.Raise(new ViewDisabledEvent(this));
        }
    }

    public abstract class ViewBase<TViewModel> : ViewBase where TViewModel : IViewModel
    {
        public TViewModel ViewModel { get; protected set; }

        protected virtual void Awake()
        {
            InitializeViewModel();
        }

        protected abstract void InitializeViewModel();

        public virtual void UpdateViewModel(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}