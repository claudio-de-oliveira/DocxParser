using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Text.Json.Nodes;

namespace JsonElement
{
    public class Variables : AbstractJsonElement
    {
        public Dictionary<string, Variable> Dictionary { get; set; } = new Dictionary<string, Variable>();

        public static Variables? Create(JsonNode? json)
        {
            if (json is null)
                return null;

            var variables = new Variables();

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json.ToJsonString());

            foreach (var v in dictionary!)
            {
                var value = JsonConvert.DeserializeObject<Variable>(((JObject)v.Value).ToString());

                if (value!.Value is JArray a)
                    value!.Value = JsonConvert.DeserializeObject<object[]>(a.ToString());

                variables.Dictionary.Add(v.Key, value!);
            }

            return variables;
        }
    }
}
