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
        public TViewModel ViewModel { get; protected set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            InitializeViewModel();
        }

        protected abstract void InitializeViewModel();

        public virtual void UpdateViewModel(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}