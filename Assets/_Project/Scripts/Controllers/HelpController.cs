using System;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.Tools.TextLogger;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Views;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class HelpController : BaseController
    {
        public HelpController(ViewManager viewManager) : base(viewManager)
        {
        }

        public void ShowHelp()
        {
            IViewInitializationParameters initParams = new HelpViewInitializationParameters(new List<ListElement>()
            {
                new ListElement("Objaśnienia ikon", null, "Pomoc",
                    () => { })
            }, ReturnToPreviousView);
            
            ViewManager.OpenView(ViewType.Help, initParams);
        }
    }
}