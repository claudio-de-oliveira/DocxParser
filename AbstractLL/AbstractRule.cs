namespace AbstractLL
{
    public class AbstractRule
    {
        public AbstractTAG LHS { get; init; }
        public AbstractTAG[] RHS { get; init; }
        public AbstractTAG[] First { get; init; }
        public AbstractTAG[] Follow { get; init; }

        public AbstractRule(AbstractTAG lHS, AbstractTAG[] rHS, AbstractTAG[] first, AbstractTAG[] follow)
        {
            LHS = lHS;
            RHS = rHS;
            First = first;
            Follow = follow;
        }
    }
}
