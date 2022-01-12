using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class ApplicationInfoViewInitializer : Initializer
    {
        public UnityAction GoBack { get; }
        public UnityAction GoForward { get; }

        public ApplicationInfoViewInitializer(UnityAction goBack, UnityAction goForward)
        {
            GoBack += goBack;
            GoForward += goForward;
        }
    }
}