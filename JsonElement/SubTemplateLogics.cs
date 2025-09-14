using System.Text.Json.Nodes;

namespace JsonElement
{
    public class SubTemplateLogics : AbstractJsonElement
    {
        public static SubTemplateLogics? Create(JsonNode? json)
        {
            if (json is null)
                return null;

            var node = new SubTemplateLogics();

            return node;
        }
    }
}
