using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class ApplicationInfoViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction GoBack { get; }
        public UnityAction GoForward { get; }

        public ApplicationInfoViewInitializationParameters(UnityAction goBack, UnityAction goForward)
        {
            GoBack += goBack;
            GoForward += goForward;
        }
    }
}