namespace AbstractLL
{
    /**
     * <summary>Abstract class defining the basic environment for the analysis</summary>
     * <typeparam name="T">Data type to be produced by the parser in success case</typeparam>
     */
    public abstract class AbstractEnvironment<T>
    {
        /**
         * <summary>Gets or sets the line of the next character of the input text to be parsed</summary>
         */
        public int CurrentRow { get; set; }
        /**
         * <summary>Gets or sets the column of the next character of the input text to be parsed</summary>
         */
        public int CurrentColumn { get; set; }
        /**
         * <summary>Gets or sets the input text to be parsed</summary>
         */
        public string[] Text { get; set; } = default!;

        protected AbstractEnvironment()
        {
            CurrentRow = CurrentColumn = 0;
            Inicializa();
            Errors = new List<string>();
        }

        /**
         * <summary>Gets a value indicating whether the end of text to be analyses has been reached</summary>
         * <value>True if end of text, false if not</value>
         */
        public bool EndOfText => CurrentRow >= Text.Length;

        /**
         * <summary>Next input character to be parsed</summary>
         */
        public char NextChar()
        {
            if (CurrentColumn >= Text[CurrentRow].Length)
            {
                CurrentRow++;
                CurrentColumn = 0;
                return '\n';

            }

            char ch = Text[CurrentRow][CurrentColumn];

            CurrentColumn++;

            return ch;
        }

        /**
         * <summary>Retracts one character of input</summary>
         */
        public void Retract()
        {
            if (CurrentColumn == 0)
            {
                CurrentRow--;
                CurrentColumn = Text[CurrentRow].Length;
            }
            else
                CurrentColumn--;
        }

        // Informações compartilhadas entre parser, scanner e semantic
        public abstract T? Result { get; }

        /**
         * <value>True if parser completes successfully, false if not</value>
         */
        public bool Success { get; set; }

        /**
         * <summary>List of errors in case of failure</summary>
         */
        public List<string> Errors { get; set; }

        /**
         * <summary>Abstract function to force specialization of environment initialization process</summary>
         */
        public abstract void Inicializa();
    }
}
