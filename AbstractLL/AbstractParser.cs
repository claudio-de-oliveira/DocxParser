namespace AbstractLL
{
    /// <summary>
    /// Classe genérica de um parser LL(1)
    /// </summary>
    public abstract class AbstractParser<T>
    {
        public static bool Dump { get; set; } = false;

        /// <summary>
        /// Lado esquerdo das regras de produção
        /// </summary>
        private AbstractRule[] rules { get; set; }

        /// <summary>
        /// Função de parsing LL(1)
        /// </summary>
        private int[][] M { get; set; }

        protected AbstractScanner<T> _scanner;
        protected AbstractSemantic<T> _semantic;

        /// <summary>
        /// Tag concreto marcando o final do texto a ser compilado.
        /// </summary>
        protected abstract AbstractTAG EndMark { get; }

        /// <summary>
        /// Indica que o texto todo já foi analizado.
        /// </summary>
        protected abstract AbstractTAG EmptyTag { get; }

        /// <summary>
        /// Resultado de uma compilação correta ou nulo
        /// </summary>
        public T? Result
        { get { return _environment.Result; } }

        private readonly AbstractEnvironment<T> _environment;

        protected AbstractEnvironment<T> Environment
        { get { return _environment; } }

        /// <summary>
        /// Construção do parser LL(1). O construtor da classe derivada deve invocar este construtor.
        /// </summary>
        /// <param name="RHS">Array dos lados direitos das regras</param>
        /// <param name="M">Função de parsing LL(1)</param>
        /// <param name="scanner">Scanner derivado de AbstractScanner</param>
        /// <param name="semantic">Semântico derivado de AbstractSemantic</param>
        protected AbstractParser(AbstractRule[] rules, int numberOfTerminals, int numberONonTerminals, AbstractScanner<T> scanner, AbstractSemantic<T> semantic, AbstractEnvironment<T> environment)
        {
            this.rules = rules;
            this._scanner = scanner;
            this._semantic = semantic;
            this._environment = environment;

            M = new int[numberONonTerminals][];

            for (int i = 0; i < numberONonTerminals; i++)
            {
                M[i] = new int[numberOfTerminals];
                for (int j = 0; j < numberOfTerminals; j++)
                    M[i][j] = -1;
            }

            // Grammar.First(rules);
            // Grammar.Follow(rules);

            for (int rule = 0; rule < rules.Length; rule++)
            {
                int row = rules[rule].LHS.ToInt();
                foreach (var first in rules[rule].First)
                {
                    int col = first.ToInt();
                    if (M[row][col] >= 0)
                        throw new Exception("Não é LL");
                    M[row][col] = rule;
                }
                foreach (var follow in rules[rule].Follow)
                {
                    if (follow != EmptyTag)
                    {
                        int col = follow.ToInt();
                        if (M[row][col] == -1)
                            M[row][col] = -2;
                    }
                }
            }
        }

        protected void Inicializa()
        {
            _scanner.Inicializa();
            _semantic.Inicializa();
            _environment.Inicializa();
        }

        /// <summary>
        /// Consulta a função de Parsing 'M'
        /// </summary>
        /// <param name="A">Não terminal no topo da pilha sintática</param>
        /// <param name="token">Próximo símbolo da entrada</param>
        /// <returns>Número da regra a ser aplicada ou uma indicação do que fazer no cso de um erro (SCAN = -1, POP = -2)</returns>
        private int Production(AbstractTAG A, AbstractToken token)
        {
            int row = (int)A;
            int col = (int)token.GetTag();

            if (row >= 0 && row < M.Length && col >= 0 && col < M[0].Length)
                return M[row][col];
            else
                return -1;
        }

        /// <summary>
        /// Insere o lado direito da regra 'p' na pilha sintática, clonando, se necessário, os tags contendo atributos
        /// </summary>
        /// <param name="stk">Pilha sintática</param>
        /// <param name="p">Regras de produção indicando o lado direito a ser empilhado</param>
        private void PushRHS(Stack<AbstractTAG> stk, int p)
        {
            for (int i = rules[p].RHS.Length - 1; i >= 0; i--)
                stk.Push(rules[p].RHS[i].Clone());
        }

        /// <summary>
        /// Ajusta os atributos durante a reescrita de um não-terminal por uma de suas definições
        /// </summary>
        /// <param name="stk">Pilha sintática contendo os tags a serem modificados</param>
        /// <param name="A">O não terminal a ser substituido</param>
        /// <param name="p">A produção contendo a definição a ser aplicada</param>
        protected abstract void SaveAttributes(Stack<AbstractTAG> stk, AbstractTAG A, int p);

        //////////////////////////////////////////////////////////////////////
        // O método de correção de erros apenas desconsidera símbolos e nunca
        // insere novos símbolos na entrada. O símbolo descartado pode ser da
        // entrada ou do otopo da pilha de parser.

        /// <summary>
        /// Função de parsing LL(1)
        /// </summary>
        /// <param name="txt">Texto a ser "compilado"</param>
        /// <returns>Resultado de uma compilação correta ou nulo.</returns>
        public object? Parse(string txt, string? filename)
        {
            //******************************************************
            //******************************************************
            //******************************************************
            //******************************************************
            // TEM QUE GARANTIR A REINICIALIZAÇÂO A PARTIR DO ZERO
            //******************************************************
            //******************************************************
            //******************************************************
            //******************************************************
            Stack<AbstractTAG> stk = new();

            Stack<AbstractTAG> variables = new();
            Stack<AbstractToken> tokens = new();

            AbstractToken token;

            bool valid = true;

            // Reinicializa o ambiente
            _environment.Inicializa();
            _environment.Text = (txt + EndMark.Name).Split('\n');

            _scanner.Inicializa();
            _semantic.Inicializa();

            PushRHS(stk, 0);

            token = _scanner.NextToken(_environment);
            if (Dump)
                Console.WriteLine(token + " ");

            try
            {
                while (true)
                {
                    AbstractTAG A = stk.Pop();

                    if (A.IsTerminal)
                    {
                        if (A == EndMark)
                        {
                            // if ( valid )
                            //     Console.WriteLine("A compilação passou.");
                            // else
                            //     Console.WriteLine("A compilação terminou com erros.");
                            // Fim da análise
                            break;
                        }
                        else if (A != token.GetTag())
                        {
                            /////////////////////////////////////////////////////////
                            // Recuperação de erros:
                            // ---------------------------------------
                            // * POP também ocorre quando o terminal no topo da pilha
                            // * é diferente do próximo símbolo da entrada

                            if (filename is not null)
                                Console.WriteLine($"Erro sintático na linha {_environment.CurrentRow}, coluna {_environment.CurrentColumn} de {filename}");
                            else
                                Console.WriteLine($"Erro sintático na linha {_environment.CurrentRow}, coluna {_environment.CurrentColumn}");
                            Console.WriteLine($"\t[{_environment.Text[_environment.CurrentRow]}]");
                            Console.WriteLine($"\t=> POP: Descartando {A}");
                            Console.WriteLine();
                            valid = false;
                            continue;
                        }
                        else // POP
                        {
                            // Esse é o POP normal

                            if (token.HasComplement())
                                tokens.Push(token);
                            token = _scanner.NextToken(_environment);
                            if (Dump)
                                Console.WriteLine(token + " ");
                        }
                    }
                    else if (A.IsVariable)
                    {
                        int p = Production(A, token);

                        variables.Push(A);

                        switch (p)
                        {
                            /////////////////////////////////////////////////////////
                            // Recuperação de erros:
                            // ---------------------------------------
                            // * Na função M, -1 significa SCAN e -2 significa POP

                            case -1: // SCAN
                                if (filename is not null)
                                    Console.WriteLine($"Erro sintático na linha {_environment.CurrentRow}, coluna {_environment.CurrentColumn} de {filename}");
                                else
                                    Console.WriteLine($"Erro sintático na linha {_environment.CurrentRow}, coluna {_environment.CurrentColumn}");
                                Console.WriteLine($"\t[{_environment.Text[_environment.CurrentRow]}]");
                                Console.WriteLine($"\t=> SCAN: Descartando M[{A}][{token}]");
                                Console.WriteLine();
                                token = _scanner.NextToken(_environment);
                                valid = false;
                                break;

                            case -2: // POP
                                if (filename is not null)
                                    Console.WriteLine($"Erro sintático na linha {_environment.CurrentRow}, coluna {_environment.CurrentColumn} de {filename}");
                                else
                                    Console.WriteLine($"Erro sintático na linha {_environment.CurrentRow}, coluna {_environment.CurrentColumn}");
                                Console.WriteLine($"\t[{_environment.Text[_environment.CurrentRow]}]");
                                Console.WriteLine($"\t=> POP: Descartando M[{A}][{token}]");
                                Console.WriteLine();
                                valid = false;
                                break;

                            default:
                                // Aplicação da regra p
                                PushRHS(stk, p);

                                // Ajuste de atributos somente se não houver erro
                                if (valid)
                                    SaveAttributes(stk, A, p);
                                break;
                        }
                    }
                    else
                    {
                        // Manipulação de atributos somente se não houver erro
                        if (valid)
                            _semantic.Execute(A, stk, tokens, _environment);
                    }
                }

                Dump = false;

                return valid ? Result : null!;
            }
            catch (Exception ex)
            {
                // Nunca deve chegar aqui!
                Console.WriteLine(ex.Message);
                // Debug.Assert(false);
                return null!;
            }
        }
    }
}