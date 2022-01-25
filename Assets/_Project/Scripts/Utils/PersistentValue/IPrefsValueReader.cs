namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public interface IPrefsValueReader<out T>
    {
        T Read(string prefsKey);
    }
}