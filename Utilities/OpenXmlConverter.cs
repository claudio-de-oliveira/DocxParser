using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

using System.Xml;

namespace Utilities
{
    public static class OpenXmlConverter
    {
        /// <summary>
        /// Converte uma string contendo InnerXml em um elemento OpenXml correspondente
        /// </summary>
        /// <param name="innerXml">String contendo o XML interno do elemento</param>
        /// <returns>OpenXmlElement correspondente ao XML fornecido</returns>
        public static OpenXmlElement ConvertInnerXmlToElement(string innerXml)
        {
            if (string.IsNullOrEmpty(innerXml))
            {
                throw new ArgumentException("O parâmetro innerXml não pode ser nulo ou vazio.", nameof(innerXml));
            }

            try
            {
                // Cria um documento XML temporário para parsing
                var xmlDoc = new XmlDocument();

                // Se o XML não contém namespaces, adiciona automaticamente
                string xmlToProcess = innerXml;
                if (!innerXml.Contains("xmlns:w"))
                {
                    xmlToProcess = $@"<root xmlns:w='http://schemas.openxmlformats.org/wordprocessingml/2006/main' 
                                        xmlns:wp='http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing'
                                        xmlns:a='http://schemas.openxmlformats.org/drawingml/2006/main'
                                        xmlns:pic='http://schemas.openxmlformats.org/drawingml/2006/picture'
                                        xmlns:r='http://schemas.openxmlformats.org/officeDocument/2006/relationships'>
                                    {innerXml}
                                 </root>";
                    xmlDoc.LoadXml(xmlToProcess);
                    return ParseXmlNodeRecursively(xmlDoc.DocumentElement!.FirstChild!);
                }
                else
                {
                    xmlDoc.LoadXml(innerXml);

                    return ParseXmlNodeRecursively(xmlDoc.DocumentElement!);
                }
            }
            catch (XmlException ex)
            {
                throw new ArgumentException($"XML inválido fornecido: {ex.Message}", nameof(innerXml), ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao converter XML para elemento OpenXml: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Método alternativo que aceita um elemento raiz específico
        /// </summary>
        /// <typeparam name="T">Tipo do elemento OpenXml desejado</typeparam>
        /// <param name="innerXml">String contendo o XML interno</param>
        /// <returns>Elemento do tipo especificado</returns>
        public static T ConvertInnerXmlToElement<T>(string innerXml) where T : OpenXmlElement, new()
        {
            var element = ConvertInnerXmlToElement(innerXml);

            if (element is T typedElement)
                return typedElement;

            // Se não for do tipo esperado, tenta criar um novo elemento do tipo T e copiar o conteúdo
            return new T { InnerXml = innerXml };
        }

        /// <summary>
        /// Converte recursivamente um XmlNode e seus filhos em elementos OpenXml tipados
        /// </summary>
        /// <param name="xmlNode">Nó XML a ser convertido</param>
        /// <returns>OpenXmlElement com tipo correto</returns>
        private static OpenXmlElement? ParseXmlNodeRecursively(XmlNode xmlNode)
        {
            if (xmlNode == null) return null;

            // Cria o elemento baseado no nome
            var element = CreateTypedElementByName(xmlNode.LocalName, xmlNode.Prefix);

            // Copia os atributos
            if (xmlNode.Attributes != null)
                foreach (XmlAttribute attr in xmlNode.Attributes)
                    if (!attr.Name.StartsWith("xmlns"))
                        element.SetAttribute(new OpenXmlAttribute(attr.Prefix, attr.LocalName, attr.NamespaceURI, attr.Value));

            // Processa os filhos recursivamente
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.Element)
                {
                    var childElement = ParseXmlNodeRecursively(childNode);
                    if (childElement != null)
                        element.AppendChild(childElement);
                }
                else if (childNode.NodeType == XmlNodeType.Text)
                {
                    // Para elementos de texto, adiciona diretamente o conteúdo
                    if (element is Text textElement)
                        textElement.Text = childNode.Value!;
                }
            }

            // Para elementos de texto que não foram processados acima
            if (element is Text text && xmlNode.InnerText != null && !xmlNode.HasChildNodes)
                text.Text = xmlNode.InnerText;

            return element;
        }

        /// <summary>
        /// Cria um elemento OpenXml tipado baseado no nome e namespace
        /// </summary>
        /// <param name="localName">Nome local do elemento</param>
        /// <param name="prefix">Prefixo do namespace</param>
        /// <returns>Elemento OpenXml do tipo correto</returns>
        private static OpenXmlElement CreateTypedElementByName(string localName, string prefix)
        {
            // Elementos do namespace 'w' (wordprocessingml)
            if (prefix == "w")
            {
                return localName.ToLowerInvariant() switch
                {
                    "p" => new Paragraph(),
                    "r" => new Run(),
                    "t" => new Text(),
                    "br" => new Break(),
                    "tab" => new TabChar(),
                    "ppr" => new ParagraphProperties(),
                    "rpr" => new RunProperties(),
                    "b" => new Bold(),
                    "i" => new Italic(),
                    "u" => new Underline(),
                    "sz" => new FontSize(),
                    "color" => new Color(),
                    "highlight" => new Highlight(),
                    "tbl" => new Table(),
                    "tr" => new TableRow(),
                    "tc" => new TableCell(),
                    "tcpr" => new TableCellProperties(),
                    "trpr" => new TableRowProperties(),
                    "tblpr" => new TableProperties(),
                    "tblgrid" => new TableGrid(),
                    "gridcol" => new GridColumn(),
                    "tcw" => new TableCellWidth(),
                    "tcborders" => new TableCellBorders(),
                    "top" => new TopBorder(),
                    "bottom" => new BottomBorder(),
                    "left" => new LeftBorder(),
                    "right" => new RightBorder(),
                    "shd" => new Shading(),
                    "jc" => new Justification(),
                    "ind" => new Indentation(),
                    "spacing" => new SpacingBetweenLines(),
                    "rfonts" => new RunFonts(),
                    "vertAlign" => new VerticalTextAlignment(),
                    "lang" => new Languages(),
                    "noProof" => new NoProof(),
                    "sectpr" => new SectionProperties(),
                    "pgSz" => new PageSize(),
                    "pgMar" => new PageMargin(),
                    "cols" => new Columns(),
                    //"docGrid" => new DocumentGrid(),
                    "bookmarkStart" => new BookmarkStart(),
                    "bookmarkEnd" => new BookmarkEnd(),
                    "hyperlink" => new Hyperlink(),
                    "fldSimple" => new SimpleField(),
                    "instrText" => new FieldCode(),
                    "fldChar" => new FieldChar(),
                    "softHyphen" => new SoftHyphen(),
                    //"nonBreakingHyphen" => new NonBreakingHyphen(),
                    "sym" => new SymbolChar(),
                    "pgNum" => new PageNumber(),
                    "cr" => new CarriageReturn(),
                    "drawing" => new Drawing(),
                    _ => new OpenXmlUnknownElement(prefix, localName, $"http://schemas.openxmlformats.org/wordprocessingml/2006/main")
                };
            }

            // Para outros namespaces ou elementos sem namespace
            return new OpenXmlUnknownElement(prefix ?? "", localName, GetNamespaceUri(prefix!));
        }

        /// <summary>
        /// Obtém a URI do namespace baseada no prefixo
        /// </summary>
        /// <param name="prefix">Prefixo do namespace</param>
        /// <returns>URI do namespace</returns>
        private static string GetNamespaceUri(string prefix)
        {
            return prefix switch
            {
                "w" => "http://schemas.openxmlformats.org/wordprocessingml/2006/main",
                "wp" => "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing",
                "a" => "http://schemas.openxmlformats.org/drawingml/2006/main",
                "pic" => "http://schemas.openxmlformats.org/drawingml/2006/picture",
                "r" => "http://schemas.openxmlformats.org/officeDocument/2006/relationships",
                _ => ""
            };
        }
    }
}
