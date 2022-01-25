using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class LogInViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction LogInEvent { get; }
        public UnityAction ContinueWithoutLogInEvent { get; }

        public LogInViewInitializationParameters(UnityAction logInEvent, UnityAction continueWithoutLogInEvent)
        {
            LogInEvent += logInEvent;
            ContinueWithoutLogInEvent += continueWithoutLogInEvent;
        }
    }
}