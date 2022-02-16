using System;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using UnityEngine.Events;


namespace PolSl.UrbanHealthPath.Controllers
{
    public class ProfileController : BaseController
    {
        private UnityAction _openTestView;

        public ProfileController(ViewManager viewManager, PopupManager popupManager, UnityAction OpenTestView) : base(
            viewManager, popupManager)
        {
            _openTestView = OpenTestView;
        }

        public void ShowProfile()
        {
            IViewInitializationParameters initParams =
                new ProfileViewInitializationParameters(new List<ListElement>()
                {
                    new ListElement("Kliknij tutaj, aby rozwiązać test sprawnościowy.", null, "Test", _openTestView),
                },ReturnToPreviousView);
            ViewManager.OpenView(ViewType.Profile, initParams);
        }
    }
    
    
}
