using Newtonsoft.Json;

using System.Text.Json.Nodes;

namespace JsonElement
{
    public class QuestionnaireJSON : AbstractJsonElement
    {

        public required Variables Variables { get; set; }
        public required Questionnaires Questionnaires { get; set; }
        public required Alert[] Alerts { get; set; }
        public required Lookups Lookups { get; set; }
        public required Functions Functions { get; set; }
        public required ListenFor ListenFor { get; set; }
        public required SubTemplate SubTemplate { get; set; }
        public required ChildTemplate[] ChildTemplate { get; set; }
        public required MultipleVariable[] MultipleVariables { get; set; }
        public required SubTemplateLogics SubTemplateLogics { get; set; }
        public string? TemplateReference { get; set; }

        public static QuestionnaireJSON? Create(JsonNode? json)
        {
            if (json is null)
                return null;

            var questionnaireJSON = new QuestionnaireJSON
            {
                Variables = Variables.Create(json[nameof(Variables)])!,
                Questionnaires = Questionnaires.Create(json[nameof(Questionnaires)])!,
                Alerts = JsonConvert.DeserializeObject<Alert[]>(json[nameof(Alerts)]!.ToJsonString())!,
                Lookups = Lookups.Create(json[nameof(Lookups)])!,
                Functions = Functions.Create(json[nameof(Functions)])!,
                ListenFor = ListenFor.Create(json[nameof(ListenFor)])!,
                SubTemplate = SubTemplate.Create(json[nameof(SubTemplate)])!,
                ChildTemplate = JsonConvert.DeserializeObject<ChildTemplate[]>(json[nameof(ChildTemplate)]!.ToJsonString())!,
                MultipleVariables = JsonConvert.DeserializeObject<MultipleVariable[]>(json[nameof(MultipleVariables)]!.ToJsonString())!,
                SubTemplateLogics = SubTemplateLogics.Create(json[nameof(SubTemplateLogics)])!,
                TemplateReference = (string)json[nameof(TemplateReference)]!
            };

            return questionnaireJSON;
        }
    }
}
