namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class IntPrefsValue : PrefsValue<int>
    {
        public IntPrefsValue(string prefsKey) : base(prefsKey, new IntPrefsValueReader(), new IntPrefsValueWriter())
        {
        }
    }
}