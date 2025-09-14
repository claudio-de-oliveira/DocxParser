using Newtonsoft.Json.Linq;

namespace JsonElement
{
    public class AbstractJsonElement
    {
        protected static string? ConvertObjetToString(object? obj)
        {
            if (obj is null)
                return null;

            if (obj is string s0)
                return s0;

            if (obj is JArray array)
            {
                string str = "";

                foreach (var s1 in array)
                    str += s1 + " ";
                return str;
            }

            return null;
        }
    }
}
