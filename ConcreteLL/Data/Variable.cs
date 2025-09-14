namespace ConcreteLL.Data
{
    public class Variable
    {
        public string? InputMethod { get; init; }
        public string? Name { get; init; }
        public string? DataType { get; init; }
        public bool? FieldOnly { get; init; }
        public int? OccursOrder { get; init; }
        public object? Prompt { get; init; }
        public string[]? Selections { get; init; }
        public object? Repeat { get; init; }
        public int Repeats { get; init; }
        public string? Definition { get; init; }
        public object? Logic { get; init; }
        public string? DefaultFormat { get; init; }
        public string? OriginalPrompt { get; init; }
        public string? Depth { get; init; }
        public bool Relevant { get; init; }
        public object? Visible { get; init; }
        public object? Value { get; init; }

        public string Declare()
        {
            return $"{DataType ?? ""} {Name} = \"{Value!}\"";
        }
    }
}
