using ConcreteLL;
using ConcreteLL.Expressions;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

using TemplateBuilder.CustomElement;

namespace TemplateBuilder
{
    public class ScannerContext
    {
        public ScannerContext? Parent { get; set; }
        public AbsExpression? Expression { get; set; }

        public ScannerContext(OpenXmlElement root, ScannerContext? parent, AbsExpression expression)
        {
            this.Parent = parent;
            this.Expression = expression;
            this.ChildList = [];
            this.Root = root;
        }

        public string Lexema { get; set; }
        public List<OpenXmlElement> ChildList { get; set; }
        public OpenXmlElement? Root { get; set; }
        public OpenXmlElement? _angleBracketXmlElement;
        public OpenXmlElement? _curlyBracketXmlElement;
        public bool _expInProgress = false;
        public bool _expStarted = false;
        public bool _newContext = true;

        public void AddRoot(OpenXmlElement root) => ChildList.Add(root);
    }

    public class ScannerDocx
    {
        public Dictionary<string, ConcreteLL.Data.Variable> Variables;
        private Stack<ScannerContext> stack;
        private ScannerContext context => stack.Peek();
        private int state { get; set; }

        public ScannerDocx(Dictionary<string, ConcreteLL.Data.Variable> variables, OpenXmlElement root)
        {
            this.Variables = variables;

            stack = new Stack<ScannerContext>();
            var parser = new Parser(Variables);
            stack.Push(new ScannerContext(root, null, (AbsExpression?)parser.Parse("True", null)!));

            state = 0;
        }

        private void CreateNewContext(OpenXmlElement root)
        {
            var parser = new Parser(Variables);
            var exp = (AbsExpression?)parser.Parse(context.Lexema, null);
            var newContext = new ScannerContext(root, context, exp!);

            context._expInProgress = false;
            context._expStarted = false;
            context.Lexema = "";
            state = 0;
            stack.Push(newContext);
        }

        Queue<ExpressionElement> expressionElements = [];
        private void CreateExpressionElement(ExpressionElement expressionElement)
        {
            expressionElements.Enqueue(expressionElement);
        }

        public ScannerContext? ScanXmlElement(OpenXmlElement xmlElement)
        {
            ScanXmlElementInterno(xmlElement);
            if (stack.Count() != 1)
                throw new Exception("Problemas com o scanner");
            return stack.Pop();
        }

        private OpenXmlElement ScanXmlElementInterno(OpenXmlElement xmlElement)
        {
            var newXmlElement = xmlElement.CloneNode(false);
            bool mark = context._newContext;
            context._newContext = false;

            OpenXmlElement parent;

            for (int i = 0; i < xmlElement.ChildElements.Count; i++)
            {
                var xmlChild = xmlElement.ChildElements.ElementAt(i);

                parent = xmlChild;

                if (xmlChild is Run run)
                {
                    var vertAlign = CheckSuperscript(run);

                    if (context._expStarted && !vertAlign)
                        CreateNewContext(xmlElement);
                    else if (context._expInProgress && vertAlign)
                        context._expStarted = true;
                }
                else if (context._expStarted && !IsChildOfRun(xmlChild))
                {
                    CreateNewContext(xmlElement);
                }

                if (xmlChild is Text xmlText)
                {
                    OpenXmlElement? newXml = ScanText(xmlText);

                    if (newXml is not null)
                    {
                        if (newXml is ValueElement valueElement)
                        {
                            var properties = xmlElement.GetFirstChild<RunProperties>();
                            if (properties is not null)
                                valueElement.AppendChild(properties.CloneNode(true));
                            newXmlElement.Append(newXml);
                            context.Lexema = "";
                        }
                        else if (newXml is ExpressionElement expressionElement)
                        {
                            if (expressionElement.CommonAncestral != xmlElement)
                                CreateExpressionElement(expressionElement);
                            else
                                newXmlElement.Append(newXml);
                        }
                        else if (!context._expInProgress)
                        {
                            newXmlElement.Append(newXml.CloneNode(true));
                            context.Lexema = "";
                        }
                    }
                }
                else
                {
                    var tmpXml = ScanXmlElementInterno(xmlChild);

                    if (tmpXml is not null)
                    {
                        if (tmpXml is ValueElement valueElement)
                        {
                            if (valueElement.CommonAncestral!.Parent != xmlElement)
                                return valueElement;
                            else
                                newXmlElement.Append(tmpXml);
                        }
                        else if (expressionElements.Any())
                        {
                            if (mark)
                            {
                                var exp = expressionElements.Dequeue();

                                if (expressionElements.Any())
                                {
                                    expressionElements.First().AppendChild(exp);
                                }
                                else
                                {
                                    context.AddRoot(exp.CloneNode(true));
                                    foreach (var xml in exp.SavedList)
                                        context.AddRoot(xml.CloneNode(true));
                                }

                                if (exp.CommonAncestral != xmlChild)
                                {
                                    if (expressionElements.Any())
                                        expressionElements.First().SavedList.Enqueue(tmpXml);
                                    else
                                        context.AddRoot(xmlChild.CloneNode(true));
                                }
                                else
                                    exp.NewCommonAncestral = tmpXml;
                            }
                            else if (expressionElements.First().CommonAncestral == xmlElement)
                            {
                                newXmlElement.AppendChild(tmpXml);
                                expressionElements.First().AppendChild(newXmlElement);
                            }
                            else
                                newXmlElement.Append(tmpXml.CloneNode(true));
                        }
                        else if (!context._expInProgress)
                        {
                            if (mark && context.Root == xmlChild.Parent)
                                context.AddRoot(tmpXml);
                            else
                                newXmlElement.Append(tmpXml.CloneNode(true));
                        }
                        else
                            newXmlElement.Append(tmpXml.CloneNode(true));
                    }
                }
            }

            return newXmlElement;
        }

        private static bool IsChildOfRun(OpenXmlElement? xml)
        {
            if (xml is null) return false;
            if (xml is Run) return true;
            return IsChildOfRun(xml.Parent);
        }

        private static bool CheckSuperscript(OpenXmlElement xml)
        {
            if (xml is null)
                return false;
            if (xml is VerticalTextAlignment vertAlign)
                return string.Equals(vertAlign.Val!, "superscript");
            for (var child = xml.FirstChild; child is not null; child = child.NextSibling())
                if (CheckSuperscript(child))
                    return true;
            return false;
        }

        private OpenXmlElement? ScanText(Text runXml)
        {
            var text = runXml.InnerText;

            for (int i = 0; i < text.Length; i++)
            {
                switch (state)
                {
                    case 0:
                        context.Lexema = "";

                        if (text[i] == '{')
                        {
                            context._curlyBracketXmlElement = runXml;
                            state = 20;
                            break;
                        }
                        if (text[i] == '<')
                        {
                            context._expInProgress = true;
                            context._angleBracketXmlElement = runXml;
                            state = 0;
                            break;
                        }
                        if (text[i] == '>')
                        {
                            var ctxt = stack.Pop();
                            var ancestralComum = EncontrarAncestralComum(context._angleBracketXmlElement!, runXml) ?? throw new Exception($"Ancestral comum não encontrado.");
                            state = 0;
                            var newXml = new ExpressionElement { Expression = ctxt.Expression, CommonAncestral = ancestralComum };
                            foreach (var x in ctxt.ChildList)
                                newXml.AppendChild(x.CloneNode(true));
                            return newXml;
                        }
                        context.Lexema += text[i];
                        state = 10;
                        break;

                    case 10:
                        if (text[i] == '{')
                        {
                            context._expInProgress = true;
                            context._curlyBracketXmlElement = runXml;
                            state = 20;
                            break;
                        }
                        if (text[i] == '<')
                        {
                            context._expInProgress = true;
                            context._angleBracketXmlElement = runXml;
                            state = 0;
                            break;
                        }
                        if (text[i] == '>')
                        {
                            var ctxt = stack.Pop();
                            var ancestralComum = EncontrarAncestralComum(context._angleBracketXmlElement!, runXml) ?? throw new Exception($"Ancestral comum não encontrado.");
                            state = 0;
                            var newXml = new ExpressionElement { Expression = ctxt.Expression, CommonAncestral = ancestralComum };
                            foreach (var x in ctxt.ChildList)
                                newXml.AppendChild(x.CloneNode(true));
                            return newXml;
                        }
                        context.Lexema += text[i];
                        state = 10;
                        break;

                    case 20:
                        if (char.IsWhiteSpace(text, i))
                        {
                            state = 20;
                            break;
                        }
                        if (char.IsLetter(text, i))
                        {
                            context.Lexema = "" + text[i];
                            state = 21;
                            break;
                        }
                        break;

                    case 21:
                        if (text[i] == '}')
                        {
                            context._expInProgress = false;

                            var parser = new Parser(Variables);
                            var exp = (AbsExpression?)parser.Parse(context.Lexema, null);
                            var ancestralComum = EncontrarAncestralComum(context._curlyBracketXmlElement!, runXml) ?? throw new Exception($"Ancestral comum não encontrado.");

                            state = 0;

                            return new ValueElement() { Expression = exp, CommonAncestral = ancestralComum };
                        }
                        context.Lexema += text[i];
                        state = 21;
                        break;

                    case 40:
                        break;

                }
            }

            if (context.Lexema.Length == 0 || context._expInProgress)
                return null;

            return new Text(context.Lexema);
        }

        private static OpenXmlElement? EncontrarAncestralComum(OpenXmlElement element1, OpenXmlElement element2)
        {
            if (element1 == null || element2 == null)
                return null;

            if (EhAncestralDe(element1, element2))
                return element1;

            if (EhAncestralDe(element2, element1))
                return element2;

            var ancestrais1 = ObterAncestrais(element1);

            var atual = element2.Parent;
            while (atual != null)
            {
                if (ancestrais1.Contains(atual))
                    return atual;
                atual = atual.Parent;
            }

            return null;
        }

        private static bool EhAncestralDe(OpenXmlElement possivelAncestral, OpenXmlElement descendente)
        {
            var atual = descendente.Parent;
            while (atual != null)
            {
                if (atual == possivelAncestral)
                    return true;
                atual = atual.Parent;
            }
            return false;
        }

        private static HashSet<OpenXmlElement> ObterAncestrais(OpenXmlElement element)
        {
            var ancestrais = new HashSet<OpenXmlElement>();
            var atual = element.Parent;

            while (atual != null)
            {
                ancestrais.Add(atual);
                atual = atual.Parent;
            }

            return ancestrais;
        }


    }
}
