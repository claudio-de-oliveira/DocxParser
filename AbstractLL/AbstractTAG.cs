using System.Diagnostics;

namespace AbstractLL
{
    /// <summary>
    /// Representação dos símbolos terminais, não terminais e ações semânticas da gramática LL(1)
    /// </summary>
    public abstract class AbstractTAG
    {
        private readonly static Guid guid = Guid.NewGuid();

        protected readonly int _tag;
        protected readonly string _name;

        // Atributos herdados
        protected object[] _inherited;

        // Mask
        protected const int SEMANTIC = 0x2000;     // 0010 0000 0000 0000
        protected const int TERMINAL = 0x4000;     // 0100 0000 0000 0000
        protected const int NONTERMINAL = 0x8000;  // 1000 0000 0000 0000

        public string Name { get { return _name; } }

        /// <summary>
        /// Valor do símbolo com os flags indicando o tipo de símbolo
        /// </summary>
        public int Tag => _tag;

        /// <summary>
        /// Valor do símbolo sem os flags indicando o tipo de símbolo
        /// </summary>
        public int ToInt() => _tag & 0x0FFF;

        /// <summary>
        /// Construtor do símbolo da gramática
        /// </summary>
        /// <param name="tag">Utilizado para indexação das tabelas de parsing</param>
        /// <param name="name">Informação para auxiliar a depuração</param>
        /// <param name="nattribs">Número de atributos associados ao símbolo</param>
        protected AbstractTAG(int tag, string name, int nattribs = 0)
        {
            this._tag = tag;
            this._name = name;
            _inherited = new object[nattribs];
        }

        public abstract AbstractTAG Clone();

        /// <summary>
        /// Retorna o i-ésimo atributo do símbolo
        /// </summary>
        /// <param name="i">Posição do atributo (0 = primeiro)</param>
        /// <returns>Atributo associado ao símbolo</returns>
        public object GetAttribute(int i)
        {
            Debug.Assert(_inherited.Length > i, $"{this} deve herdar, pelo menos, {i + 1} atributo(s)!");
            Debug.Assert(_inherited[i] != null, $"{this}.Inherited[{i}] não deve ser nulo!");

            return _inherited[i];
        }

        /// <summary>
        /// Seta o i-ésimo atributo do símbolo
        /// </summary>
        /// <param name="i">Posição do atributo (0 = primeiro)</param>
        /// <param name="attribute">Atributo</param>
        public void SetAttribute(int i, object attribute)
        {
            Debug.Assert(_inherited.Length > i, $"{this} deve herdar, pelo menos, {i + 1} atributo(s)!");

            _inherited[i] = attribute;
        }

        public bool TestNullAttribute(int index)
        {
            Debug.Assert(_inherited.Length > index, $"{this} deve herdar, pelo menos, {index + 1} atributo(s)!");

            return _inherited[index] == null;
        }

        /// <summary>
        /// True se o símbolo for um terminal
        /// </summary>
        public bool IsTerminal => (Tag & 0xC000) == TERMINAL;
        /// <summary>
        /// True se o símbolo for uma variável
        /// </summary>
        public bool IsVariable => (Tag & 0xC000) == NONTERMINAL;
        public bool IsSemantic => (Tag & 0xC000) == SEMANTIC;

        /// <summary>
        /// Converte o símbolo para int - usado para indexação de tabelas
        /// </summary>
        public static explicit operator int(AbstractTAG x) => x.Tag & 0x2FFF;

        public override string ToString()
        {
            if (IsTerminal)
                return $"{this._name}";
            else if (IsVariable)
                return $"<{this._name}>";
            else
                return this._name;
        }

        #region Teste de igualdade
        public override bool Equals(object? obj)
        {
            // If parameter is null return false
            if (obj is null)
                return false;

            // If parameter cannot be cast to Tag return false
            object? p = obj as AbstractTAG;
            if (p is null)
                return false;

            // Return true if the fields match
            return Tag == ((AbstractTAG)p).Tag;
        }

        public bool Equals(AbstractTAG p)
        {
            // If parameter is null return false
            if (p is null)
                return false;

            // Return true if the fields match
            return Tag == p.Tag;
        }

        public override int GetHashCode()
        {
            return Tag.GetHashCode() ^ guid.GetHashCode();
        }

        public static bool operator ==(AbstractTAG a, AbstractTAG b)
        {
            // If both are null, or both are same instance, return true
            if (ReferenceEquals(a, b))
                return true;

            if (a is null)
                return false;

            // Return true if the fields match
            return a.Equals(b);
        }

        public static bool operator !=(AbstractTAG a, AbstractTAG b)
        {
            return !(a == b);
        }
        #endregion
    }
}
