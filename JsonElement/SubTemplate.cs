
using System.Text.Json.Nodes;

namespace JsonElement
{
    public class SubTemplate : AbstractJsonElement
    {
        public static SubTemplate? Create(JsonNode? json)
        {
            if (json is null)
                return null;

            var node = new SubTemplate();

            return node;
        }
    }
}
