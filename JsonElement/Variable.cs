namespace JsonElement
{
    public class Variable : AbstractJsonElement
    {
        public string? InputMethod { get; set; }
        public string? Name { get; set; }
        public string? DataType { get; set; }
        public bool? FieldOnly { get; set; }
        public int? OccursOrder { get; set; }
        public object? Prompt { get; set; }
        public string[]? Selections { get; set; }
        public object? Repeat { get; set; }
        public int Repeats { get; set; }
        public string? Definition { get; set; }
        public object? Logic { get; set; }
        public string? DefaultFormat { get; set; }
        public string? OriginalPrompt { get; set; }
        public string? Depth { get; set; }
        public bool Relevant { get; set; }
        public object? Visible { get; set; }
        public object? Value { get; set; }

        public string? GetPrompt() => AbstractJsonElement.ConvertObjetToString(Prompt);
        public string? GetValue() => AbstractJsonElement.ConvertObjetToString(Value);

        public string Declare()
        {
            return $"{DataType ?? ""} {Name} = \"{Value!}\"";
        }
    }
}
