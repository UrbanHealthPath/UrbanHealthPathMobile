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
            
            //TODO: We should find a place to unsubscribe
            ViewManager.ViewOpened += HandleViewChange;
        }
        
        protected void ReturnToPreviousView()
        {
            (ViewType viewType, IViewInitializationParameters initParams) lastView = ViewManager.History.GetLastView();
            ViewManager.OpenView(lastView.viewType, lastView.initParams);
        }

        protected virtual void HandleViewChange(ViewType type)
        {
        }
    }
}