namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public interface IPrefsValueWriter<in T>
    {
        void Write(string prefsKey, T value);
    }
}