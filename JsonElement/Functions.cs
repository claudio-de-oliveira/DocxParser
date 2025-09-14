using System.Text.Json.Nodes;

namespace JsonElement
{
    public class Functions : AbstractJsonElement
    {
        public static Functions? Create(JsonNode? json)
        {
            if (json is null)
                return null;

            var node = new Functions();

            return node;
        }
    }
}
