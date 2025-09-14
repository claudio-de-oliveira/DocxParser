using System.Text.Json.Nodes;

namespace JsonElement
{
    public class ListenFor : AbstractJsonElement
    {
        public static ListenFor? Create(JsonNode? json)
        {
            if (json is null)
                return null;

            var node = new ListenFor();

            return node;
        }
    }
}
