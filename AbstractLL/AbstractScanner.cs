namespace AbstractLL
{
    /// <summary>
    /// Classe genérica de um scanner LL(1)
    /// </summary>
    public abstract class AbstractScanner<T>
    {
        protected AbstractScanner()
        {
            Inicializa();
        }
        /// <summary>
        /// Converte sequência de caracteres em sequências de símbolos (tokens)
        /// </summary>
        /// <param name="text">Sequência de caracteres de entrada</param>
        /// <returns>Próximo símbolo (token) da entrada</returns>
        /// 
        public abstract AbstractToken NextToken(AbstractEnvironment<T> environment);
        public virtual void Inicializa()
        { }
    }
}
