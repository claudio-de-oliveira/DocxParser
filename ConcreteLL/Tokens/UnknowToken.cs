using AbstractLL;

namespace ConcreteLL.Tokens
{
    internal class UnknowToken : AbstractToken
    {
        public string Text { get; set; }

        public UnknowToken(string text)
            : base(Tag.UNKNOW)
        {
            this.Text = text;
        }

        public override string ToString() => $"{GetTag()}: {Text}";
    }
}
