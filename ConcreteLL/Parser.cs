using AbstractLL;

using ConcreteLL.Expressions;

namespace ConcreteLL
{
    public class Parser : AbstractParser<AbsExpression>
    {
        private static readonly AbstractRule[] rules = new AbstractRule[]
            {
                // 0. & <Start> ::= <FmtExp> @Done "\#"
                new AbstractRule(
                    Tag.Start,
                    new AbstractTAG[] { Tag.FmtExp, Tag._Done, Tag.ENDMARK },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR, Tag.NOT, Tag.IF },
                    new AbstractTAG[] { Tag.EMPTY }
                    ),
                // 1. & <FmtExp> ::= <Exp> <FmtExp'>
                new AbstractRule(
                    Tag.FmtExp,
                    new AbstractTAG[] { Tag.Exp, Tag.FmtExp_ },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR, Tag.NOT, Tag.IF },
                    new AbstractTAG[] { Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 2. & <Exp> ::= "if" <Exp> @Test "then" <Exp> @Then "else" <Exp> @Else @If
                new AbstractRule(
                    Tag.Exp,
                    new AbstractTAG[] { Tag.IF, Tag.Exp, Tag._Test, Tag.THEN, Tag.Exp, Tag._Then, Tag.ELSE, Tag.Exp, Tag._Else, Tag._If },
                    new AbstractTAG[] { Tag.IF },
                    new AbstractTAG[] { Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 3. & <Exp> ::= <Disj>
                new AbstractRule(
                    Tag.Exp,
                    new AbstractTAG[] { Tag.Disj },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR, Tag.NOT },
                    new AbstractTAG[] { Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 4. & <Disj> ::= <Conj> <Disj'>
                new AbstractRule(
                    Tag.Disj,
                    new AbstractTAG[] { Tag.Conj, Tag.Disj_ },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR, Tag.NOT },
                    new AbstractTAG[] { Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 5. & <Disj'> ::= "or" <Conj> @Or <Disj'>
                new AbstractRule(
                    Tag.Disj_,
                    new AbstractTAG[] { Tag.OR, Tag.Conj, Tag._Or, Tag.Disj_ },
                    new AbstractTAG[] { Tag.OR },
                    new AbstractTAG[] { Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 6. & <Disj'> ::= @Echo
                new AbstractRule(
                    Tag.Disj_,
                    new AbstractTAG[] { Tag._Echo },
                    new AbstractTAG[] { Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK },
                    Array.Empty<AbstractTAG>()
                    ),
                // 7. & <Conj> ::= <Neg> <Conj'>
                new AbstractRule(
                    Tag.Conj,
                    new AbstractTAG[] { Tag.Neg, Tag.Conj_ },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR, Tag.NOT },
                    new AbstractTAG[] { Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 8. & <Conj'> ::= "and" <Neg> @And <Conj'>
                new AbstractRule(
                    Tag.Conj_,
                    new AbstractTAG[] { Tag.AND, Tag.Neg, Tag._And, Tag.Conj_ },
                    new AbstractTAG[] { Tag.AND },
                    new AbstractTAG[] { Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 9. & <Conj'> ::= @Echo
                new AbstractRule(
                    Tag.Conj_,
                    new AbstractTAG[] { Tag._Echo },
                    new AbstractTAG[] { Tag.RPAR, Tag.THEN, Tag.ELSE, Tag.OR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK },
                    Array.Empty<AbstractTAG>()
                    ),
                // 10. & <Neg> ::= "not" <Neg> @Not
                new AbstractRule(
                    Tag.Neg,
                    new AbstractTAG[] { Tag.NOT, Tag.Neg, Tag._Not },
                    new AbstractTAG[] { Tag.NOT },
                    new AbstractTAG[] { Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 11. & <Neg> ::= <Rel> 
                new AbstractRule(
                    Tag.Neg,
                    new AbstractTAG[] { Tag.Rel },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR },
                    new AbstractTAG[] { Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 12. & <Rel> ::= <Add> <Rel'> 
                new AbstractRule(
                    Tag.Rel,
                    new AbstractTAG[] { Tag.Add, Tag.Rel_ },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR },
                    new AbstractTAG[] { Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 13. & <Rel'> ::= "RelOp" @RelOp <Add> @Rel
                new AbstractRule(
                    Tag.Rel_,
                    new AbstractTAG[] { Tag.RELOP, Tag._RelOp, Tag.Add, Tag._Rel },
                    new AbstractTAG[] { Tag.RELOP },
                    new AbstractTAG[] { Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 14. & <Rel'> ::= @Echo 
                new AbstractRule(
                    Tag.Rel_,
                    new AbstractTAG[] { Tag._Echo },
                    new AbstractTAG[] { Tag.RPAR, Tag.THEN, Tag.ELSE, Tag.OR, Tag.AND, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK },
                    Array.Empty<AbstractTAG>()
                    ),
                // 15. & <Add> ::= <Multiply> <Add'> 
                new AbstractRule(
                    Tag.Add,
                    new AbstractTAG[] { Tag.Multiply, Tag.Add_ },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR },
                    new AbstractTAG[] { Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 16. & <Add'> ::= "AddOp" @AddOp <Multiply> @Add <Add'> 
                new AbstractRule(
                    Tag.Add_,
                    new AbstractTAG[] { Tag.ADDOP, Tag._AddOp, Tag.Multiply, Tag._Add, Tag.Add_ },
                    new AbstractTAG[] { Tag.ADDOP },
                    new AbstractTAG[] { Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 17. & <Add'> ::= @Echo 
                new AbstractRule(
                    Tag.Add_,
                    new AbstractTAG[] { Tag._Echo },
                    new AbstractTAG[] { Tag.RPAR, Tag.THEN, Tag.ELSE, Tag.OR, Tag.AND, Tag.RELOP, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK },
                    Array.Empty<AbstractTAG>()
                    ),
                // 18. & <Multiply> ::= <Factor> <Multiply'> 
                new AbstractRule(
                    Tag.Multiply,
                    new AbstractTAG[] { Tag.Factor, Tag.Multiply_ },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR },
                    new AbstractTAG[] { Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 19. & <Multiply'> ::= "MulOp" @MulOp <Factor> @Mul <Multiply'>  
                new AbstractRule(
                    Tag.Multiply_,
                    new AbstractTAG[] { Tag.MULOP, Tag._MulOp, Tag.Factor, Tag._Mul, Tag.Multiply_ },
                    new AbstractTAG[] { Tag.MULOP },
                    new AbstractTAG[] { Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 20. & <Multiply'> ::= @Echo 
                new AbstractRule(
                    Tag.Multiply_,
                    new AbstractTAG[] { Tag._Echo },
                    new AbstractTAG[] { Tag.RPAR, Tag.THEN, Tag.ELSE, Tag.OR, Tag.AND, Tag.RELOP, Tag.COMMA, Tag.ADDOP, Tag.FORMAT, Tag.ENDMARK  },
                    Array.Empty<AbstractTAG>()
                    ),
                // 21. & <Factor> ::= "Function" @Function <Function'>
                new AbstractRule(
                    Tag.Factor,
                    new AbstractTAG[] { Tag.FUNCTION, Tag._Function, Tag.Function_ },
                    new AbstractTAG[] { Tag.FUNCTION },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 22. & <Factor> ::= "Variable" @Variable
                new AbstractRule(
                    Tag.Factor,
                    new AbstractTAG[] { Tag.VARIABLE, Tag._Variable },
                    new AbstractTAG[] { Tag.VARIABLE },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 23. & <Factor> ::= "Integer" @Integer 
                new AbstractRule(
                    Tag.Factor,
                    new AbstractTAG[] { Tag.INTEGER, Tag._Integer },
                    new AbstractTAG[] { Tag.INTEGER },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 24. & <Factor> ::= "Decimal" @Decimal 
                new AbstractRule(
                    Tag.Factor,
                    new AbstractTAG[] { Tag.DECIMAL, Tag._Decimal },
                    new AbstractTAG[] { Tag.DECIMAL },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 25. & <Factor> ::= "Literal" @Literal 
                new AbstractRule(
                    Tag.Factor,
                    new AbstractTAG[] { Tag.LITERAL, Tag._Literal },
                    new AbstractTAG[] { Tag.LITERAL },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 26. & <Factor> ::= "True" @True
                new AbstractRule(
                    Tag.Factor,
                    new AbstractTAG[] { Tag.TRUE, Tag._True },
                    new AbstractTAG[] { Tag.TRUE },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 27. & <Factor> ::= "False" @False 
                new AbstractRule(
                    Tag.Factor,
                    new AbstractTAG[] { Tag.FALSE, Tag._False },
                    new AbstractTAG[] { Tag.FALSE },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 28. & <Factor> ::= "(" <Exp> @Skip  ")" 
                new AbstractRule(
                    Tag.Factor,
                    new AbstractTAG[] { Tag.LPAR, Tag.Exp, Tag._Skip, Tag.RPAR },
                    new AbstractTAG[] { Tag.LPAR },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 29. & <Factor> ::= "Select" <Selections> @Select 
                new AbstractRule(
                    Tag.Factor,
                    new AbstractTAG[] { Tag.SELECT, Tag.Selections, Tag._Select },
                    new AbstractTAG[] { Tag.SELECT },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 30. & <Function'> ::= "(" <List> @Skip ")" @Call 
                new AbstractRule(
                    Tag.Function_,
                    new AbstractTAG[] { Tag.LPAR, Tag.List, Tag._Skip, Tag.RPAR, Tag._Call },
                    new AbstractTAG[] { Tag.LPAR },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 31. <Function'> ::= <Exp> @Parameter @Call 
                new AbstractRule(
                    Tag.Function_,
                    new AbstractTAG[] { Tag.Exp, Tag._Parameter, Tag._Call },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.NOT, Tag.IF,  },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 32. <Function'> ::= @NoParameter @Call 
                new AbstractRule(
                    Tag.Function_,
                    new AbstractTAG[] { Tag._NoParameter, Tag._Call },
                    new AbstractTAG[] { Tag.RPAR, Tag.THEN, Tag.ELSE, Tag.OR, Tag.AND, Tag.RELOP, Tag.COMMA, Tag.ADDOP, Tag.MULOP, Tag.FORMAT, Tag.ENDMARK  },
                    Array.Empty<AbstractTAG>()
                    ),
                // 33. & <List> ::= <Exp> @CreateList <List'> 
                new AbstractRule(
                    Tag.List,
                    new AbstractTAG[] { Tag.Exp, Tag._CreateList, Tag.List_ },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR, Tag.NOT, Tag.IF },
                    new AbstractTAG[] { Tag.RPAR }
                    ),
                // 34. & <List> ::= @EmptyList 
                new AbstractRule(
                    Tag.List,
                    new AbstractTAG[] { Tag._EmptyList },
                    new AbstractTAG[] { Tag.RPAR },
                    new AbstractTAG[] { Tag.RPAR }
                    ),
                // 35. & <List'> ::= "," <Exp> @InsertList <List'>
                new AbstractRule(
                    Tag.List_,
                    new AbstractTAG[] { Tag.COMMA, Tag.Exp, Tag._InsertList, Tag.List_ },
                    new AbstractTAG[] { Tag.COMMA },
                    new AbstractTAG[] { Tag.RPAR }
                    ),
                // 36. & <List'> ::= @Echo 
                new AbstractRule(
                    Tag.List_,
                    new AbstractTAG[] { Tag._Echo },
                    new AbstractTAG[] { Tag.RPAR },
                    Array.Empty<AbstractTAG>()
                    ),
                // 37. <Selections> ::= <SelItem> @FirstItem <Selections'>
                new AbstractRule(
                    Tag.Selections,
                    new AbstractTAG[] { Tag.SelItem, Tag._FirstItem, Tag.Selections_ },
                    new AbstractTAG[] { Tag.VARIABLE },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 38. <Selections'> ::= "," <SelItem> @InsertItem <Selections'>
                new AbstractRule(
                    Tag.Selections_,
                    new AbstractTAG[] { Tag.COMMA, Tag.SelItem, Tag._InsertItem, Tag.Selections_ },
                    new AbstractTAG[] { Tag.COMMA },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 39. <Selections'> ::= @Echo
                new AbstractRule(
                    Tag.Selections_,
                    new AbstractTAG[] { Tag._Echo },
                    new AbstractTAG[] { Tag.RPAR, Tag.THEN, Tag.ELSE, Tag.OR, Tag.AND, Tag.RELOP, Tag.ADDOP, Tag.MULOP, Tag.FORMAT, Tag.ENDMARK  },
                    Array.Empty<AbstractTAG>()
                    ),
                // 40. <SelItem> ::= "Variable" @Variable <SelItem'>
                new AbstractRule(
                    Tag.SelItem,
                    new AbstractTAG[] { Tag.VARIABLE, Tag._Variable, Tag.SelItem_ },
                    new AbstractTAG[] { Tag.VARIABLE },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 41. <SelItem'> ::= "From" <Selections> @SelectItem
                new AbstractRule(
                    Tag.SelItem_,
                    new AbstractTAG[] { Tag.FROM, Tag.Selections, Tag._SelectItem },
                    new AbstractTAG[] { Tag.FROM },
                    new AbstractTAG[] { Tag.MULOP, Tag.ADDOP, Tag.AND, Tag.OR, Tag.THEN, Tag.ELSE, Tag.RPAR, Tag.COMMA, Tag.FORMAT, Tag.ENDMARK }
                    ),
                // 42. <SelItem'> ::= @Echo
                new AbstractRule(
                    Tag.SelItem_,
                    new AbstractTAG[] { Tag._Echo },
                    new AbstractTAG[] { Tag.RPAR, Tag.THEN, Tag.ELSE, Tag.OR, Tag.AND, Tag.RELOP, Tag.COMMA, Tag.ADDOP, Tag.MULOP, Tag.FORMAT, Tag.ENDMARK },
                    Array.Empty<AbstractTAG>()
                    ),
                // 43. <SelItem'> ::= Format <Exp> @Format
                new AbstractRule(
                    Tag.FmtExp_,
                    new AbstractTAG[] { Tag.FORMAT, Tag.Exp, Tag._Format },
                    new AbstractTAG[] { Tag.FORMAT },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR, Tag.NOT, Tag.IF }
                    ),
                // 44. <FmtExp'> ::= @Echo
                new AbstractRule(
                    Tag.FmtExp_,
                    new AbstractTAG[] { Tag._Echo },
                    new AbstractTAG[] { Tag.SELECT, Tag.FUNCTION, Tag.VARIABLE, Tag.INTEGER, Tag.DECIMAL, Tag.LITERAL, Tag.TRUE, Tag.FALSE, Tag.LPAR, Tag.NOT, Tag.IF, Tag.ENDMARK },
                    Array.Empty<AbstractTAG>()
                    ),

            };

        protected override AbstractTAG EndMark => Tag.ENDMARK;

        protected override AbstractTAG EmptyTag => Tag.EMPTY;

        public Parser(Dictionary<string, Data.Variable> variables)
            : base(
                  rules,
                  Tag.NumberOfTerminals.ToInt(),
                  Tag.NumberOfVariables.ToInt(),
                  new Scanner(),
                  new Semantic(),
                  new Environment(variables)
                  )
        {
            /* othing more todo */
        }

        protected override void SaveAttributes(Stack<AbstractTAG> stk, AbstractTAG A, int p)
        {
            // Ajustes de parâmetros durante a aplicação de uma regra
            switch (p)
            {
                // 6. & <Disj'> ::= @Echo
                // 9. & <Conj'> ::= @Echo
                // 14. & <Rel'> ::= @Echo 
                // 17. & <Add'> ::= @Echo 
                // 20. & <Multiply'> ::= @Echo 
                // 36. & <List'> ::= @Echo 
                // 39. <Selections'> ::= @Echo
                // 42. <SelItem'> ::= @Echo
                // 44. <FmtExp'> ::= @Echo
                case 6:
                case 9:
                case 14:
                case 17:
                case 20:
                case 36:
                case 39:
                case 42:
                case 44:
                    stk.Peek().SetAttribute(0, A.GetAttribute(0));
                    break;
                // 5. & <Disj'> ::= "or" <Conj> @Or <Disj'>
                // 8. & <Conj'> ::= "and" <Neg> @And <Conj'>
                case 5:
                case 8:
                    stk.ElementAt(2).SetAttribute(1, A.GetAttribute(0));
                    break;
                // 13. & <Rel'> ::= "RelOp @Op <Exp> @Rel
                // 16. & <Add'> ::= "AddOp" @Op <Multiply> @Add <Add'> 
                // 19. & <Multiply'> ::= "MulOp" @Op <Factor> @Mul <Multiply'>  
                case 13:
                case 16:
                case 19:
                    stk.ElementAt(3).SetAttribute(2, A.GetAttribute(0));
                    break;
                // 30. & <Function'> ::= "(" <List> @Skip ")" @Call 
                case 30:
                    stk.ElementAt(4).SetAttribute(1, A.GetAttribute(0));
                    break;
                // 31. <Function'> ::= <Exp> @Parameter @Call 
                case 31:
                    stk.ElementAt(2).SetAttribute(1, A.GetAttribute(0));
                    break;
                // 32. <Function'> ::= @NoParameter @Call 
                case 32:
                    stk.ElementAt(1).SetAttribute(1, A.GetAttribute(0));
                    break;
                // 35. & <List'> ::= "," <Exp> @InsertList <List'>
                case 35:
                    stk.ElementAt(2).SetAttribute(1, A.GetAttribute(0));
                    break;
                // 38. <Selections'> ::= "," <SelItem> @InsertItem <Selections'>
                case 38:
                    stk.ElementAt(2).SetAttribute(1, A.GetAttribute(0));
                    break;
                // 41. <SelItem'> ::= "From" <Selections> @SelectItem
                case 41:
                    stk.ElementAt(2).SetAttribute(1, A.GetAttribute(0));
                    break;
                // 43. <FmtExp'> ::= "Format" <Exp> @Format
                case 43:
                    stk.ElementAt(2).SetAttribute(1, A.GetAttribute(0));
                    break;
            }
        }
    }
}
