namespace JsonElement
{
    public class Questionnaire : AbstractJsonElement
    {
        public string? Name { get; set; }
        public object[]? Group { get; set; }
        public object? Visible { get; set; }
    }
}
