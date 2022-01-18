using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class ApplicationInfoViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction LeftButton { get; }
        public UnityAction RightButton { get; }

        public ApplicationInfoViewInitializationParameters(UnityAction leftButton, UnityAction rightButton)
        {
            LeftButton += leftButton;
            RightButton += rightButton;
        }
    }
}