using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class StringPrefsValueReader : IPrefsValueReader<string>
    {
        public string Read(string prefsKey)
        {
            return PlayerPrefs.GetString(prefsKey);
        }
    }
}