using System.Text.Json.Nodes;

namespace JsonElement
{
    public class Data : AbstractJsonElement
    {
        public QuestionnaireJSON? QuestionnaireJSON { get; set; }
        public string? Answers { get; set; }
        public string? JsonMatcher { get; set; }
        public string? IdRedis { get; set; }

        public static Data? Create(JsonNode? json)
        {
            if (json is null)
                return null;

            var data = new Data
            {
                Answers = (string?)json["answers"],
                JsonMatcher = (string?)json["jsonMatcher"],
                IdRedis = (string?)json["idRedis"],
                QuestionnaireJSON = QuestionnaireJSON.Create(json["questionnaireJSON"]!)
            };

            return data;
        }
    }
}
