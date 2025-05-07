using Core.View;

namespace Core.Controller
{
    public abstract class ControllerBase
    {
        protected readonly ViewBase View;

        protected ControllerBase(ViewBase view)
        {
            View = view;
        }
    }

    public abstract class ControllerBase<TView> : ControllerBase where TView : ViewBase
    {
        protected ControllerBase(TView view) : base(view)
        {
        }
    }
}