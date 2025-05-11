namespace Core.Infrastructure.ViewController
{
    public abstract class Controller
    {
        protected readonly View View;

        protected Controller(View view)
        {
            View = view;
        }
    }

    public abstract class Controller<TView> : Controller where TView : View
    {
        protected Controller(TView view) : base(view)
        {
        }
    }
}