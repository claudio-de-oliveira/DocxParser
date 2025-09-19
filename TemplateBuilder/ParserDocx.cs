using ConcreteLL.Expressions;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

using TemplateBuilder.CustomElement;

namespace TemplateBuilder
{
    internal class ParserDocx
    {
        private Dictionary<string, ConcreteLL.Data.Variable> variables;
        private Dictionary<string, object> history;

        public ParserDocx(Dictionary<string, ConcreteLL.Data.Variable> variables)
        {
            this.variables = variables;
            this.history = [];
        }

        public OpenXmlElement? Parse(OpenXmlElement xmlElement)
        {
            var scannerDocx = new ScannerDocx(variables, xmlElement);
            var context = scannerDocx.ScanXmlElement(xmlElement);
            var body = new Body();

            foreach (var element in context!.ChildList)
            {
                var childClone = ParseInterno(element, 0);
                TrayAppendChildren(body, childClone);
            }

            return body;
        }

        private OpenXmlElement? ParseInterno(OpenXmlElement xmlElement, int index)
        {
            if (xmlElement is ExpressionElement exp)
            {
                switch (exp.Expression)
                {
                    case RepeatExp:
                        {
                            var times = (long)exp.Expression!.Evaluate(variables, history); // retorna o número de vezes a repetir

                            var clone = ((ExpressionElement)xmlElement).CommonAncestral!.CloneNode(false);

                            for (int i = 0; i < times; i++)
                            {
                                foreach (var childElement in xmlElement.ChildElements)
                                {
                                    OpenXmlElement childClone;

                                    if (childElement is ExpressionElement childExpression)
                                    {
                                        if (childExpression.CommonAncestral is Body)
                                        {
                                            childClone = childExpression.CommonAncestral!.CloneNode(false);
                                            foreach (var element in childExpression.ChildElements)
                                            {
                                                var child = ParseInterno(element, index + i);
                                                TrayAppendChildren(childClone, child);
                                            }
                                        }
                                        else
                                        {
                                            var child = ParseInterno(childExpression, index + i);

                                            childClone = childExpression.CommonAncestral!.Parent!.CloneNode(false);
                                            TrayAppendChildren(clone, childClone);
                                        }
                                    }
                                    else
                                    {
                                        childClone = childElement.CloneNode(false);
                                        foreach (var element in childElement.ChildElements)
                                        {
                                            var child = ParseInterno(element, index + i);
                                            TrayAppendChildren(childClone, child);
                                        }
                                    }
                                    TrayAppendChildren(clone, childClone);
                                }
                            }

                            return clone;
                        }
                    case UnrepeatedExp:
                        {
                            var repeat = (bool)exp.Expression!.Evaluate(variables, history); // retorna true se já imprimiu

                            if (!repeat)
                            {
                                //var clone = childElement.CloneNode(false);

                                //foreach (var element in xmlElement.ChildElements)
                                //{
                                //    var childList = Parse(element, index);
                                //    var clone = element.CloneNode(false);
                                //    childList.ForEach(x => clone.Append(x.CloneNode(true)));
                                //    newChildren.Add(clone);
                                //}
                            }

                            return null;
                        }
                    default:
                        {
                            var result = exp.Expression!.Evaluate(variables, history);

                            if ((bool)result)
                            {
                                OpenXmlElement clone;

                                if (((ExpressionElement)xmlElement).CommonAncestral is Body)
                                    clone = ((ExpressionElement)xmlElement).CommonAncestral!.CloneNode(false);
                                else
                                    clone = ((ExpressionElement)xmlElement).CommonAncestral!.Parent!.CloneNode(false);

                                foreach (var element in ((ExpressionElement)xmlElement).ChildElements)
                                {
                                    var childClone = ParseInterno(element, index);
                                    TrayAppendChildren(clone, childClone);
                                }

                                return clone;
                            }

                            return null;
                        }
                }
            }
            else if (xmlElement is ValueElement val)
            {
                var result = val.Expression!.Evaluate(variables, history);

                while (result is Array)
                    result = ((Array)result).GetValue(index);

                return new Text() { Text = result!.ToString()! };
            }
            else if (xmlElement is OpenXmlLeafElement)
            {
                return xmlElement.CloneNode(true);
            }
            else
            {
                var clone = xmlElement.CloneNode(false);

                foreach (var element in xmlElement.ChildElements)
                {
                    var childClone = ParseInterno(element, index);
                    TrayAppendChildren(clone, childClone);
                }

                return clone;
            }

            throw new NotImplementedException();
        }

        private static void TrayAppendChildren(OpenXmlElement target, OpenXmlElement? source)
        {
            if (source is null) return;

            if (target.GetType() == source.GetType())
                foreach (var x in source.ChildElements)
                    target.AppendChild(x.CloneNode(true));
            else
                target.AppendChild(source);
        }
    }
}
