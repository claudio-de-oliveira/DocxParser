namespace AbstractLL
{
    /// <summary>
    /// Classe genérica de um analisador semântico LL(1)
    /// </summary>
    public abstract class AbstractSemantic<T>
    {
        /// <summary>
        /// Função para o tratamento das ações semânticas
        /// </summary>
        /// <param name="action">Ação semântica a ser executada</param>
        /// <param name="stk">Pilha de parsing</param>
        /// <param name="tokens">Símbolos da entrada contendo informações complementares que não foram processadas ainda</param>
        protected AbstractSemantic()
        { Inicializa(); }
        public abstract void Execute(AbstractTAG action, Stack<AbstractTAG> stk, Stack<AbstractToken> tokens, AbstractEnvironment<T> state);
        public abstract void Inicializa();
    }
}
