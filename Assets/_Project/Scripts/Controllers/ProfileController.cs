using System;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;


namespace PolSl.UrbanHealthPath.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(ViewManager viewManager, PopupManager popupManager) : base(viewManager, popupManager) {}

        public void ShowProfile()
        {
            IViewInitializationParameters initParams =
                new ProfileViewInitializationParameters(new List<ListElement>()
                {
                    new ListElement("Kliknij tutaj, aby rozwiązać test sprawnościowy.", null, "Test", OpenTestView),
                },ReturnToPreviousView);
            ViewManager.OpenView(ViewType.Profile, initParams);
        }

        private void OpenTestView()
        {
            
        }
    }
    
    
}
