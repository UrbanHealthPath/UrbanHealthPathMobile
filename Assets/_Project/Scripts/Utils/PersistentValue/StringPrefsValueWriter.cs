using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class StringPrefsValueWriter : IPrefsValueWriter<string>
    {
        public void Write(string prefsKey, string value)
        {
            PlayerPrefs.SetString(prefsKey, value);
        }
    }
}