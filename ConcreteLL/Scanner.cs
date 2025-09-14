using AbstractLL;

using ConcreteLL.Expressions;
using ConcreteLL.Tokens;

namespace ConcreteLL
{
    internal class Scanner : AbstractScanner<AbsExpression>
    {
        private static readonly Dictionary<string, AbstractToken> reservedWords = new()
        {
            { "alt",  Token.ALT },
            { "and",  Token.AND },
            { "answered",  Token.ANSWERED },
            { "as",  Token.AS },
            { "attach",  Token.ATTACH },
            { "attribute",  Token.ATTRIBUTE },
            { "authornote",  Token.AUTHORNOTE },
            { "by",  Token.BY },
            { "capitals",  Token.CAPITALS },
            { "cell",  Token.CELL },
            { "cloaked", Token.CLOAKED },
            { "collect",  Token.COLLECT },
            { "collectvalues",  Token.COLLECTVALUES },
            { "committed",  Token.COMMITTED },
            { "datatype",  Token.DATATYPE },
            { "deferred",  Token.DEFERRED },
            { "definite",  Token.DEFINITE },
            { "doctitle",  Token.DOCTITLE },
            { "document", Token.DOCUMENT },
            { "else",  Token.ELSE },
            { "every",  Token.EVERY },
            { "exists",  Token.EXISTS },
            { "export",  Token.EXPORT },
            { "expressiontext",  Token.EXPRESSIONTEXT },
            { "false",  Token.FALSE },
            { "foreach",  Token.FOREACH },
            { "format",  Token.FORMAT },
            { "from",  Token.FROM },
            { "hyperlink",  Token.HYPERLINK },
            { "if", Token.IF },                           // test that a value equals another value
            { "ifknownelse",  Token.IFKNOWNELSE },
            { "include",  Token.INCLUDE },
            { "is",  Token.RELOP },                       // test that a value equals another value
            { "isatleast",  Token.RELOP},                 // test that a value is more than or equal to another value
            { "isatmost",  Token.RELOP },                 // test that a value is less than or equal to another value
            { "islessthan",  Token.RELOP },               // test that a value is less than another value
            { "ismorethan",  Token.RELOP },               // test that a value is more than another value
            { "isnot",  Token.RELOP },                    // test that a value is not equal to another value
            { "known", Token.KNOWN },
            { "knowntrue",  Token.KNOWNTRUE },
            { "label",  Token.LABEL },
            { "list",  Token.LIST },
            { "lower",  Token.LOWER },
            { "mark",  Token.MARK },
            { "nonmutualand",  Token.NONMUTUALAND },
            { "nonmutualor",  Token.NONMUTUALOR },
            { "nonrepeated",  Token.NONREPEATED },
            { "not", Token.NOT },
            { "note",  Token.NOTE },
            { "now",  Token.NOW },
            { "occurrence",  Token.OCCURRENCE },
            { "onlyoninput",  Token.ONLYONINPUT },
            { "onlyonoutput",  Token.ONLYONOUTPUT },
            { "onlyother",  Token.ONLYOTHER },
            { "or",  Token.OR },
            { "other", Token.OTHER },
            { "otherselections",  Token.OTHERSELECTIONS },
            { "picture",  Token.PICTURE },
            { "prefix",  Token.PREFIX },
            { "prescribedselections",  Token.PRESCRIBEDSELECTIONS },
            { "proper",  Token.PROPER },
            { "punctuation",  Token.PUNCTUATION },
            { "ref", Token.REF },
            { "reference",  Token.REFERENCE },
            { "relevance",  Token.RELEVANCE },
            { "repeat",  new FunctionToken("repeat") },
            { "repeatcontext",  Token.REPEATCONTEXT },
            { "repeatcounter",  Token.REPEATCOUNTER },
            { "select", Token.SELECT },
            { "selectionoptions",  Token.SELECTIONOPTIONS },
            { "sensitive",  Token.SENSITIVE },
            { "simplify",  Token.SIMPLIFY },
            { "spanrelevance",  Token.SPANRELEVANCE },
            { "style",  Token.STYLE },
            { "sure",  Token.SURE },
            { "template", Token.TEMPLATE },
            { "templaterelevance",  Token.TEMPLATERELEVANCE },
            { "textfile",  Token.TEXTFILE },
            { "then",  Token.THEN },
            { "to",  Token.TO },
            { "today",  Token.TODAY },
            { "true",  Token.TRUE },
            { "unrepeated", new FunctionToken("unrepeated") },
            { "upper",  Token.UPPER },
            { "using",  Token.USING },
            { "value", Token.VALUE },
            { "where",  Token.WHERE },
            { "with", Token.WITH },
            { "xor", Token.XOR },
        };

        public override AbstractToken NextToken(AbstractEnvironment<AbsExpression> environment)
        {
            string lexema = "";
            int state = 0;
            char ch;

            while (true)
            {
                switch (state)
                {
                    case 0:
                        ch = environment.NextChar();

                        if (char.IsWhiteSpace(ch))
                        {
                            state = 0;
                            break;
                        }
                        if (char.IsLetter(ch))
                        {
                            /*
                             * If the name of your variable includes a space or any character 
                             * outside of the letter characters from the Latin-1 Supplement or 
                             * Latin Extended-A Unicode character set, then the Dictionary 
                             * Editor will automatically add single quotes around the variable 
                             * when the variable is added into a field in the template.
                             */
                            lexema += "" + ch;
                            state = 1;
                            break;
                        }
                        if (char.IsDigit(ch))
                        {
                            lexema += "" + ch;
                            state = 40;
                            break;
                        }
                        if (ch == '\'')
                        {
                            /* Quoting Variable Names
                             * --------------------------
                             * Remember that Variable names containing certain characters (including spaces) 
                             * will need to be quoted using single quotes:
                             * 'Tax Regulations Apply'
                             * For a list of characters that do not need quoting see the Characters used in 
                             * Identifier names topic in the online documentation. 
                             */
                            lexema += "" + ch;
                            state = 3;
                            break;
                        }
                        if (ch == '\"')
                        {
                            /* Single vs Double quotes
                             * --------------------------
                             * Note that the value from text selection variables are double-quoted in 
                             * the business rule. If the text selection variable also had spaces or 
                             * other characters that need to be quoted in identifiers, the business 
                             * rule would look like this:
                             * ['Contract Region' Is "North America" This Agreement is subject to the 
                             * laws of the state of New York
                             * ]
                             */
                            state = 10;
                            break;
                        }
                        if (ch == '+' || ch == '-')
                        {
                            lexema += "" + ch;
                            state = 20;
                            break;
                        }
                        if (ch == '*')
                        {
                            lexema += "" + ch;
                            state = 21;
                            break;
                        }
                        if (ch == '/')
                        {
                            lexema += "" + ch;
                            state = 22;
                            break;
                        }
                        if (ch == '(')
                        {
                            state = 30;
                            break;
                        }
                        if (ch == ')')
                        {
                            state = 31;
                            break;
                        }
                        if (ch == ',')
                        {
                            state = 32;
                            break;
                        }
                        if (ch == '#')
                        {
                            state = 100;
                            break;
                        }
                        // TEXT
                        lexema = "" + ch;
                        state = 999;
                        break;

                    case 1:
                        ch = environment.NextChar();

                        if (char.IsLetterOrDigit(ch))
                        {
                            lexema += ch;
                            state = 1;
                            break;
                        }
                        state = 2;
                        break;

                    case 2:
                        environment.Retract();
                        /* Reserved words.
                         * ---------------------------------------
                         * There are a number of reserved words which variable names cannot contain 
                         * such as AND and NOT. The full list is as follows:
                         * alt, and, answered, as, attach, attribute, authornote, by, capitals, cell, 
                         * cloaked, collect, collectvalues, committed, datatype, deferred, definite, doctitle, 
                         * document, else, every, exists, export, expressiontext, false, foreach, format, 
                         * from, hyperlink, if, ifknownelse, include, is, isatleast, isatmost, islessthan, 
                         * ismorethan, isnot, known, knowntrue, label, list, lower, mark, nonmutualand, 
                         * nonmutualor, nonrepeated, not, note, now, occurrence, onlyoninput, onlyonoutput, 
                         * onlyother, or, other, otherselections, picture, prefix, prescribedselections,
                         * proper, punctuation, ref, reference, relevance, repeat, repeatcontext, 
                         * repeatcounter, select, selectionoptions, sensitive, simplify, spanrelevance, 
                         * style, sure, template, templaterelevance, textfile, then, to, today, true, 
                         * unrepeated, upper, using, value, where, with, xor 
                         */
                        if (((Environment)environment).SymbolTable.ContainsKey(lexema.ToLower()))
                            return ((Environment)environment).SymbolTable[lexema.ToLower()];

                        if (reservedWords.ContainsKey(lexema.ToLower()))
                        {
                            AbstractToken token = reservedWords[lexema.ToLower()];
                            if (token.GetTag() == Tag.RELOP)
                                return new RelOpToken(lexema);
                            if (token.GetTag() == Tag.ADDOP)
                                return new AddOpToken(lexema);
                            if (token.GetTag() == Tag.MULOP)
                                return new MulOpToken(lexema);
                            return token;
                        }

                        if (((Environment)environment).Variables.ContainsKey(lexema))
                            return new VariableToken(lexema);
                        else
                            return new FunctionToken(lexema);

                    case 3:
                        /* Escape character and reserved words.
                         * ---------------------------------------
                         * If you include a single quote as part of the variable Name, the Variable 
                         * Editor will automatically add a backslash character to escape the quote: \'
                         * Similarly, if the name includes a backslash, the variable editor will 
                         * prefix this with a further backslash character \\
                         * If you add fields into the template as you type, you must remember to 
                         * escape quotes and backslashes where necessary.
                         * Additionally, you cannot use the following symbols as the variable name (but 
                         * they can be included as part of the variable name if quoted):
                         * + - * / // , ( ) # ? .
                         */
                        ch = environment.NextChar();

                        if (char.IsLetterOrDigit(ch))
                        {
                            lexema += ch;
                            state = 4;
                            break;
                        }
                        if (char.IsWhiteSpace(ch))
                        {
                            lexema += ch;
                            state = 3;
                            break;
                        }
                        if ("+-*/,()#?.".Contains(ch)) // caracter //?
                        {
                            lexema += ch;
                            state = 4;
                            break;
                        }
                        if (ch == '\\')
                        {
                            state = 4;
                            break;
                        }
                        state = 999;
                        break;

                    case 4:
                        /* Escape character.
                         * ---------------------------------------
                         * If you include a single quote as part of the variable Name, the Variable 
                         * Editor will automatically add a backslash character to escape the quote: \'
                         * Similarly, if the name includes a backslash, the variable editor will 
                         * prefix this with a further backslash character \\
                         * If you add fields into the template as you type, you must remember to 
                         * escape quotes and backslashes where necessary.
                         * Additionally, you cannot use the following symbols as the variable name (but 
                         * they can be included as part of the variable name if quoted):
                         * + - * / // , ( ) # ? .
                         */
                        ch = environment.NextChar();

                        if (char.IsLetterOrDigit(ch))
                        {
                            lexema += ch;
                            state = 4;
                            break;
                        }
                        if (char.IsWhiteSpace(ch))
                        {
                            lexema += ch;
                            state = 4;
                            break;
                        }
                        if ("+-*/,()#?.".Contains(ch)) // caracter //?
                        {
                            lexema += ch;
                            state = 4;
                            break;
                        }
                        if (ch == '\\')
                        {
                            state = 5;
                            break;
                        }
                        if (ch == '\'')
                        {
                            state = 6;
                            break;
                        }
                        state = 999;
                        break;

                    case 5:
                        ch = environment.NextChar();

                        if (ch == '\\')
                        {
                            lexema += ch;
                            state = 4;
                            break;
                        }
                        state = 999;
                        break;

                    case 6:
                        if (((Environment)environment).Variables.ContainsKey(lexema))
                            return new VariableToken(lexema);
                        else
                            return new UnknowToken(lexema);

                    case 9:
                        environment.Retract();
                        if (((Environment)environment).Variables.ContainsKey(lexema))
                            return new VariableToken(lexema);
                        else
                            return new UnknowToken(lexema);

                    case 10:
                        ch = environment.NextChar();

                        if (ch == '\"')
                        {
                            state = 11;
                            break;
                        }
                        if (ch != '\n')
                        {
                            lexema += ch;
                            state = 10;
                            break;
                        }
                        state = 999;
                        break;

                    case 11:
                        return new LiteralToken(lexema);

                    case 20:
                        return new AddOpToken(lexema);

                    case 21:
                        return new MulOpToken(lexema);

                    case 22:
                        ch = environment.NextChar();

                        if (ch == '/')
                        {
                            lexema += "" + ch;
                            state = 23;
                            break;
                        }
                        state = 24;
                        break;

                    case 23:
                        return new MulOpToken(lexema);

                    case 24:
                        environment.Retract();
                        return new MulOpToken(lexema);

                    case 30:
                        return Token.LPAR;

                    case 31:
                        return Token.RPAR;

                    case 32:
                        return Token.COMMA;

                    case 40:
                        ch = environment.NextChar();

                        if (char.IsDigit(ch))
                        {
                            lexema += "" + ch;
                            state = 40;
                            break;
                        }
                        if (ch == '.')
                        {
                            lexema += "" + ch;
                            state = 41;
                            break;
                        }
                        state = 45;
                        break;

                    case 41:
                        ch = environment.NextChar();

                        if (char.IsDigit(ch))
                        {
                            lexema += "" + ch;
                            state = 42;
                            break;
                        }
                        state = 999;
                        break;

                    case 42:
                        ch = environment.NextChar();

                        if (char.IsDigit(ch))
                        {
                            lexema += "" + ch;
                            state = 42;
                            break;
                        }
                        state = 49;
                        break;

                    case 45: // Integer
                        environment.Retract();
                        return new IntegerToken(long.Parse(lexema));

                    case 49: // Decimal
                        environment.Retract();
                        return new DecimalToken(double.Parse(lexema));

                    case 100:
                        return Token.ENDMARK;

                    case 999:
                        environment.Retract();

                        ch = environment.NextChar();

                        // Invalid Token
                        return new UnknowToken(ch + "");
                }
            }
        }
    }
}
