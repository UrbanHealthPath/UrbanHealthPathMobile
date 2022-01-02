using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData
{
    public interface IJsonFileReader
    {
        JToken ReadJsonFromFile(string filePath);
    }
}