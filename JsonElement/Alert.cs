namespace JsonElement
{
    public class Alert : AbstractJsonElement
    {
        public bool Compulsory { get; set; }
        public string? Definition { get; set; }
        public string? Message { get; set; }
        public bool Visible { get; set; }
        public string[]? Variables { get; set; }
    }
}
