namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class HeaderInitializer : Initializer
    {
        public string Text { get; private set; }

        public HeaderInitializer(string text)
        {
            Text = text;
        }
    }
}