using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class LogInViewInitializer : Initializer
    {
        public UnityAction LogInEvent { get; }
        public UnityAction ContinueWithoutLogInEvent { get; }

        public LogInViewInitializer(UnityAction logInEvent, UnityAction continueWithoutLogInEvent)
        {
            LogInEvent += logInEvent;
            ContinueWithoutLogInEvent += continueWithoutLogInEvent;
        }
    }
}