using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

using System.Diagnostics;

namespace Utilities
{
    public static class TextSplitter
    {
        private static void SplitGreaterThan(OpenXmlElement xmlElement, OpenXmlElement root)
        {
            // Substitui um Text contendo uma sequência de n caracteres '>' por uma
            // sequência de n Text's, cada qual com um único caractere '>'
            if (xmlElement is Text text)
            {
                string t = "";

                for (int i = 0; i < text.Text.Length; i++)
                {
                    if (text.Text[i] != '>' && text.Text[i] != '<')
                    {
                        t += text.Text[i];
                    }
                    else if (t.Length > 0 && text.Text[i] == '>')
                    {
                        root.AppendChild(new Text(t));
                        root.AppendChild(new Text(">"));
                        t = "";
                    }
                    else if (t.Length > 0 && text.Text[i] == '<')
                    {
                        root.AppendChild(new Text(t));
                        root.AppendChild(new Text("<"));
                        t = "";
                    }
                    else if (text.Text[i] == '>')
                    {
                        root.AppendChild(new Text(">"));
                        t = "";
                    }
                    else if (text.Text[i] == '<')
                    {
                        root.AppendChild(new Text("<"));
                        t = "";
                    }
                    else
                        Debugger.Break();
                }
                if (t.Length > 0)
                    root.AppendChild(new Text(t));
            }
            else if (xmlElement is not ProofError)
            {
                var clone = xmlElement.CloneNode(false);
                root.AppendChild(clone);

                foreach (var child in xmlElement.ChildElements)
                    SplitGreaterThan(child, clone);
            }
        }

        public static OpenXmlElement SplitCommandMarks(OpenXmlElement xmlElement)
        {
            // Primeiramente separa todos os caracteres '>' em Text próprios
            var root = xmlElement.CloneNode(false);
            foreach (var child in xmlElement.ChildElements)
                SplitGreaterThan(child, root);

            return root.CloneNode(true);
        }
    }
}
