using Newtonsoft.Json;

using System.Text.Json.Nodes;

namespace JsonElement
{
    public class Questionnaires : AbstractJsonElement
    {
        public Dictionary<string, Questionnaire> Dictionary { get; set; } = new Dictionary<string, Questionnaire>();

        public static Questionnaires? Create(JsonNode? json)
        {
            if (json is null)
                return null;

            var node = new Questionnaires();

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json.ToJsonString());

            foreach (var v in dictionary!)
            {
                var value = JsonConvert.DeserializeObject<Questionnaire>(v.Value.ToString()); ;

                node.Dictionary.Add(v.Key, value);
            }

            return node;
        }
    }
}
