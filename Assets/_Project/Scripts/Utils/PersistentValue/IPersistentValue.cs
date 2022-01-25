namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public interface IPersistentValue<T>
    {
        T Value { get; set; }
    }
}