namespace AbstractLL
{
    /// <summary>
    /// Empacota os símbolos terminais da gramática LL(1) e possíveis informações complementares
    /// </summary>
    public abstract class AbstractToken
    {
        private readonly static Guid guid = Guid.NewGuid();

        protected AbstractTAG _tag;

        /// <summary>
        /// Construtor do token
        /// </summary>
        /// <param name="tag">Símbolo terminal da gramática</param>
        protected AbstractToken(AbstractTAG tag)
        { this._tag = tag; }

        /// <summary>
        /// Símbolo terminal da gramática
        /// </summary>
        // public AbstractTAG Tag => _tag;
        public AbstractTAG GetTag() => _tag;

        /// <summary>
        /// true se o token contém alguma informação complementar
        /// </summary>
        public virtual bool HasComplement() => false;

        public override string ToString() => $"{GetTag()}";

        #region Teste de igualdade
        public override bool Equals(object? obj)
        {
            // If parameter is null return false
            if (obj is null)
                return false;

            // If parameter cannot be cast to Tag return false
            object? p = obj as AbstractToken;
            if (p is null)
                return false;

            // Return true if the fields match
            return GetTag() == ((AbstractToken)p).GetTag();
        }

        public bool Equals(AbstractToken p)
        {
            // If parameter is null return false
            if (p is null)
                return false;

            // Return true if the fields match
            return GetTag() == p.GetTag();
        }

        public override int GetHashCode()
        {
            return guid.GetHashCode() ^ GetTag().GetHashCode();
        }

        public static bool operator ==(AbstractToken a, AbstractToken b)
        {
            // If both are null, or both are same instance, return true
            if (ReferenceEquals(a, b))
                return true;

            if (a is null)
                return false;

            // Return true if the fields match
            return a.Equals(b);
        }

        public static bool operator !=(AbstractToken a, AbstractToken b)
        {
            return !(a == b);
        }
        #endregion
    }
}
