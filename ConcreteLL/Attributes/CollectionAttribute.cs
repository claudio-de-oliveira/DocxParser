using AbstractLL;

namespace ConcreteLL.Attributes
{
    internal class CollectionAttribute : AbstractAttribute
    {
        public List<AbstractAttribute> Attributes { get; init; }

        public CollectionAttribute()
        {
            Attributes = new List<AbstractAttribute>();
        }

        public CollectionAttribute(List<AbstractAttribute> attributes)
        {
            Attributes = attributes;
        }

        public override AbstractResult? ConvertToResult()
        {
            throw new NotImplementedException();
        }
    }
}
