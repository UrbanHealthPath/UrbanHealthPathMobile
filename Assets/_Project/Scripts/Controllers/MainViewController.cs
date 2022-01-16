using PolSl.UrbanHealthPath.UserInterface.Views;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class MainViewController
    {
        private readonly ViewManager _viewManager;

        public MainViewController(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }
    }
}