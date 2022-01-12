namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class StringPrefsValue : PrefsValue<string>
    {
        public StringPrefsValue(string prefsKey, string defaultValue) : base(prefsKey, defaultValue, new StringPrefsValueReader(), new StringPrefsValueWriter())
        {
        }

        public StringPrefsValue(string prefsKey) : base(prefsKey, new StringPrefsValueReader(), new StringPrefsValueWriter())
        {
        }
    }
}