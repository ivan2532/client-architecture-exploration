using Core.ViewModel;
using UnityEngine;

namespace Core.View
{
    public abstract class ViewBase : MonoBehaviour
    {
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