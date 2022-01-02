using System.Collections.Generic;

namespace PolSl.UrbanHealthPath
{
    public interface IParser<in T, out Y>
    {
        Y Parse(T parsedValue);
    }
}
