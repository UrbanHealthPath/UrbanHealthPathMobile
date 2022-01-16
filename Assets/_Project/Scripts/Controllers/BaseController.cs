using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;

namespace PolSl.UrbanHealthPath.Controllers
{
    public abstract class BaseController
    {
        protected ViewManager ViewManager { get; }

        protected BaseController(ViewManager viewManager)
        {
            ViewManager = viewManager;
        }

        protected void ReturnToPreviousView()
        {
            (ViewType viewType, IViewInitializationParameters initParams) lastView = ViewManager.History.GetLastView();
            ViewManager.OpenView(lastView.viewType, lastView.initParams);
        }
    }
}