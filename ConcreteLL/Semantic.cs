using AbstractLL;

using ConcreteLL.Expressions;
using ConcreteLL.Tokens;

namespace ConcreteLL
{
    internal class Semantic : AbstractSemantic<AbsExpression>
    {
        public override void Execute(AbstractTAG action, Stack<AbstractTAG> stk, Stack<AbstractToken> tokens, AbstractEnvironment<AbsExpression> env)
        {
            if (action == Tag._Echo)
            {
                stk.Peek().SetAttribute(0, action.GetAttribute(0));
            }
            else if (action == Tag._Done)
            {
                ((Environment)env).result = (AbsExpression)action.GetAttribute(0);
            }
            else if (action == Tag._Test)
            {
                stk.ElementAt(6).SetAttribute(2, action.GetAttribute(0));
            }
            else if (action == Tag._Then)
            {
                stk.ElementAt(3).SetAttribute(1, action.GetAttribute(0));
            }
            else if (action == Tag._Else)
            {
                stk.Peek().SetAttribute(0, action.GetAttribute(0));
            }
            else if (action == Tag._If)
            {
                var elseExp = (AbsExpression)action.GetAttribute(0);
                var thenExp = (AbsExpression)action.GetAttribute(1);
                var testExp = (AbsExpression)action.GetAttribute(2);

                stk.Peek().SetAttribute(0, new IfExp(testExp, thenExp, elseExp));
            }
            else if (action == Tag._Or)
            {
                var left = (AbsExpression)action.GetAttribute(0);
                var right = (AbsExpression)action.GetAttribute(1);

                stk.Peek().SetAttribute(0, new OrExp(left, right));
            }
            else if (action == Tag._And)
            {
                var left = (AbsExpression)action.GetAttribute(0);
                var right = (AbsExpression)action.GetAttribute(1);

                stk.Peek().SetAttribute(0, new AndExp(left, right));
            }
            else if (action == Tag._Not)
            {
                var exp = (AbsExpression)action.GetAttribute(0);

                stk.Peek().SetAttribute(0, new NotExp(exp));
            }
            else if (action == Tag._AddOp)
            {
                stk.ElementAt(1).SetAttribute(1, ((AddOpToken)tokens.Pop()).Value);
            }
            else if (action == Tag._Add)
            {
                var left = (AbsExpression)action.GetAttribute(2);
                var op = (string)action.GetAttribute(1);
                var right = (AbsExpression)action.GetAttribute(0);

                stk.Peek().SetAttribute(0, new AddExp(op, left, right));
            }
            else if (action == Tag._MulOp)
            {
                stk.ElementAt(1).SetAttribute(1, ((MulOpToken)tokens.Pop()).Value);
            }
            else if (action == Tag._Mul)
            {
                var left = (AbsExpression)action.GetAttribute(2);
                var op = (string)action.GetAttribute(1);
                var right = (AbsExpression)action.GetAttribute(0);

                stk.Peek().SetAttribute(0, new MulExp(op, left, right));
            }
            else if (action == Tag._RelOp)
            {
                stk.ElementAt(1).SetAttribute(1, ((RelOpToken)tokens.Pop()).Value);
            }
            else if (action == Tag._Rel)
            {
                var left = (AbsExpression)action.GetAttribute(2);
                var op = (string)action.GetAttribute(1);
                var right = (AbsExpression)action.GetAttribute(0);

                stk.Peek().SetAttribute(0, new RelExp(op, left, right));
            }
            else if (action == Tag._Variable)
            {
                var token = (VariableToken)tokens.Pop();

                if (((Environment)env).Variables.ContainsKey(token.Value))
                {
                    var v = ((Environment)env).Variables[token.Value];
                    stk.Peek().SetAttribute(0, new VariableExp(v));
                }
                else
                {
                    throw new Exception($"Erro na ação semântica {action}");
                }
            }
            else if (action == Tag._Integer)
            {
                var token = (IntegerToken)tokens.Pop();
                stk.Peek().SetAttribute(0, new IntegerExp(token.Value));
            }
            else if (action == Tag._Decimal)
            {
                var token = (DecimalToken)tokens.Pop();
                stk.Peek().SetAttribute(0, new DecimalExp(token.Value));
            }
            else if (action == Tag._Literal)
            {
                var tk = tokens.Pop();
                stk.Peek().SetAttribute(0, new LiteralExp(((LiteralToken)tk).Value));
            }
            else if (action == Tag._Skip)
            {
                stk.ElementAt(1).SetAttribute(0, action.GetAttribute(0));
            }
            else if (action == Tag._EmptyList)
            {
                stk.Peek().SetAttribute(0, new List<AbsExpression>());
            }
            else if (action == Tag._CreateList)
            {
                var list = new List<AbsExpression>() { (AbsExpression)action.GetAttribute(0) };
                stk.Peek().SetAttribute(0, list);
            }
            else if (action == Tag._InsertList)
            {
                var list = (List<AbsExpression>)action.GetAttribute(1);
                list.Add((AbsExpression)action.GetAttribute(0));
                stk.Peek().SetAttribute(0, list);
            }
            else if (action == Tag._True)
            {
                stk.Peek().SetAttribute(0, new TrueExp());
            }
            else if (action == Tag._False)
            {
                stk.Peek().SetAttribute(0, new FalseExp());
            }
            else if (action == Tag._NoParameter)
            {
                stk.Peek().SetAttribute(0, new List<AbsExpression>());
            }
            else if (action == Tag._Parameter)
            {
                var parameter = (AbsExpression)action.GetAttribute(0);
                var list = new List<AbsExpression> { parameter };
                stk.Peek().SetAttribute(0, list);
            }
            else if (action == Tag._Function)
            {
                var function = (FunctionToken)tokens.Pop();
                stk.Peek().SetAttribute(0, function.Value);
            }
            else if (action == Tag._Call)
            {
                var function = (string)action.GetAttribute(1);
                var parameters = (List<AbsExpression>)action.GetAttribute(0);

                if (string.Compare(function, "repeat", true) == 0)
                {
                    if (parameters.Count != 1)
                        throw new Exception();

                    stk.Peek().SetAttribute(0, new RepeatExp(parameters[0]));
                }
                else if (string.Compare(function, "unrepeated", true) == 0)
                {
                    if (parameters.Count != 1)
                        throw new Exception();

                    stk.Peek().SetAttribute(0, new UnrepeatedExp(parameters[0]));
                }
                else
                    stk.Peek().SetAttribute(0, new FunctionExp(function, parameters));
            }
            else if (action == Tag._Format)
            {
                var exp = (AbsExpression)action.GetAttribute(1);
                var format = (LiteralExp)action.GetAttribute(0);

                stk.Peek().SetAttribute(0, new FormatExp(exp, format));
            }
            else if (action == Tag._Select)
            {
                throw new NotImplementedException();
            }
            else if (action == Tag._SelectItem)
            {
                var variable = (VariableExp)action.GetAttribute(1);
                var items = (List<AbsExpression>)action.GetAttribute(0);
                stk.Peek().SetAttribute(0, new SelectionItemExp(variable, items));

                throw new NotImplementedException();
            }
            else if (action == Tag._FirstItem)
            {
                var item = (AbsExpression)action.GetAttribute(0);
                var items = new List<AbsExpression>();
                items.Add(item);
                stk.Peek().SetAttribute(0, items);

                throw new NotImplementedException();
            }
            else if (action == Tag._InsertItem)
            {
                var item = (AbsExpression)action.GetAttribute(0);
                var items = (List<AbsExpression>)action.GetAttribute(1);
                items.Add(item);
                stk.Peek().SetAttribute(0, items);

                throw new NotImplementedException();
            }
            else
            {
                throw new Exception();
            }

        }

        public override void Inicializa()
        {
        }
    }
}
