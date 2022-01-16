using System;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.Tools.TextLogger;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Views;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class HelpController
    {
        private readonly ViewManager _viewManager;

        public HelpController(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        public void ShowHelp(Action returnFromView)
        {
            IViewInitializationParameters initParams = new HelpViewInitializationParameters(new List<ListElement>()
            {
                new ListElement("Objaśnienia ikon", null, "Pomoc",
                    () => { })
            }, () => returnFromView.Invoke());
            
            _viewManager.OpenView(ViewType.Help, initParams);
        }
    }
}