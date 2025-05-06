using Core.View;

namespace Core.Controller
{
    public abstract class ControllerBase
    {
        protected readonly ViewBase ViewBase;

        protected ControllerBase(ViewBase viewBase)
        {
            ViewBase = viewBase;
        }
    }

    public abstract class ControllerBase<TView> : ControllerBase where TView : ViewBase
    {
        protected readonly TView View;

        protected ControllerBase(TView view) : base(view)
        {
            View = view;
        }
    }
}