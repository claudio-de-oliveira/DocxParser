using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Utilities
{
    /// <summary>
    /// Opções para configurar a comparação de elementos OpenXml
    /// </summary>
    public class ComparisonOptions
    {
        /// <summary>
        /// Ignora diferenças de case em texto e atributos
        /// </summary>
        public bool IgnoreCase { get; set; } = false;

        /// <summary>
        /// Ignora whitespace em texto e atributos
        /// </summary>
        public bool IgnoreWhitespace { get; set; } = false;

        /// <summary>
        /// Ignora diferenças de namespace
        /// </summary>
        public bool IgnoreNamespaces { get; set; } = false;

        /// <summary>
        /// Ignora declarações de namespace (xmlns)
        /// </summary>
        public bool IgnoreNamespaceDeclarations { get; set; } = true;

        /// <summary>
        /// Ignora elementos vazios
        /// </summary>
        public bool IgnoreEmptyElements { get; set; } = false;

        /// <summary>
        /// Lista de nomes de atributos a serem ignorados na comparação
        /// </summary>
        public HashSet<string> IgnoreAttributes { get; set; } = new HashSet<string>();

        /// <summary>
        /// Lista de tipos de elementos a serem ignorados na comparação
        /// </summary>
        public HashSet<Type> IgnoreElementTypes { get; set; } = new HashSet<Type>();
    }

    /// <summary>
    /// Resultado detalhado de uma comparação
    /// </summary>
    public class ComparisonResult
    {
        private readonly List<string> _differences = new List<string>();

        /// <summary>
        /// Indica se os elementos são iguais
        /// </summary>
        public bool AreEqual => _differences.Count == 0;

        /// <summary>
        /// Lista de diferenças encontradas
        /// </summary>
        public IReadOnlyList<string> Differences => _differences.AsReadOnly();

        /// <summary>
        /// Adiciona uma diferença à lista
        /// </summary>
        internal void AddDifference(string difference)
        {
            _differences.Add(difference);
        }

        /// <summary>
        /// Retorna um resumo das diferenças
        /// </summary>
        public override string ToString()
        {
            if (AreEqual)
                return "Elementos são iguais";

            return $"Encontradas {_differences.Count} diferença(s):\n" + string.Join("\n", _differences);
        }
    }


    public static class OpenXmlEqualityComparer
    {
        /// <summary>
        /// Testa se dois elementos OpenXml são iguais
        /// </summary>
        /// <param name="element1">Primeiro elemento a ser comparado</param>
        /// <param name="element2">Segundo elemento a ser comparado</param>
        /// <param name="options">Opções de comparação (opcional)</param>
        /// <returns>True se os elementos forem iguais, False caso contrário</returns>
        public static bool AreEqual(OpenXmlElement element1, OpenXmlElement element2, ComparisonOptions options = null)
        {
            options ??= new ComparisonOptions();

            // Se ambos são null, são iguais
            if (element1 == null && element2 == null)
                return true;

            // Se apenas um é null, são diferentes
            if (element1 == null || element2 == null)
                return false;

            // Se são referências ao mesmo objeto, são iguais
            if (ReferenceEquals(element1, element2))
                return true;

            return CompareElements(element1, element2, options);
        }

        /// <summary>
        /// Compara dois elementos recursivamente
        /// </summary>
        /// <param name="element1">Primeiro elemento</param>
        /// <param name="element2">Segundo elemento</param>
        /// <param name="options">Opções de comparação</param>
        /// <returns>True se forem iguais</returns>
        private static bool CompareElements(OpenXmlElement element1, OpenXmlElement element2, ComparisonOptions options)
        {
            // 1. Comparar tipos
            if (element1.GetType() != element2.GetType())
                return false;

            // 2. Comparar local name e namespace
            if (!options.IgnoreNamespaces)
            {
                if (element1.LocalName != element2.LocalName || element1.NamespaceUri != element2.NamespaceUri)
                    return false;
            }
            else
            {
                if (element1.LocalName != element2.LocalName)
                    return false;
            }

            // 3. Comparar atributos
            if (!CompareAttributes(element1, element2, options))
                return false;

            // 4. Comparar conteúdo texto (para elementos de texto)
            if (!CompareTextContent(element1, element2, options))
                return false;

            // 5. Comparar elementos filhos
            if (!CompareChildren(element1, element2, options))
                return false;

            return true;
        }

        /// <summary>
        /// Compara os atributos de dois elementos
        /// </summary>
        private static bool CompareAttributes(OpenXmlElement element1, OpenXmlElement element2, ComparisonOptions options)
        {
            var attrs1 = GetFilteredAttributes(element1, options);
            var attrs2 = GetFilteredAttributes(element2, options);

            if (attrs1.Count != attrs2.Count)
                return false;

            foreach (var attr1 in attrs1)
            {
                var attr2 = attrs2.FirstOrDefault(a =>
                    a.LocalName == attr1.LocalName &&
                    (options.IgnoreNamespaces || a.NamespaceUri == attr1.NamespaceUri));

                if (attr2 == null)
                    return false;

                if (!CompareAttributeValues(attr1.Value, attr2.Value, options))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Obtém atributos filtrados baseado nas opções
        /// </summary>
        private static List<OpenXmlAttribute> GetFilteredAttributes(OpenXmlElement element, ComparisonOptions options)
        {
            var attributes = element.GetAttributes().ToList();

            if (options.IgnoreAttributes?.Any() == true)
            {
                attributes = attributes.Where(a => !options.IgnoreAttributes.Contains(a.LocalName)).ToList();
            }

            if (options.IgnoreNamespaceDeclarations)
            {
                attributes = attributes.Where(a => !a.LocalName.StartsWith("xmlns")).ToList();
            }

            return attributes;
        }

        /// <summary>
        /// Compara valores de atributos
        /// </summary>
        private static bool CompareAttributeValues(string value1, string value2, ComparisonOptions options)
        {
            if (options.IgnoreWhitespace)
            {
                value1 = value1?.Trim();
                value2 = value2?.Trim();
            }

            if (options.IgnoreCase)
            {
                return string.Equals(value1, value2, StringComparison.OrdinalIgnoreCase);
            }

            return string.Equals(value1, value2);
        }

        /// <summary>
        /// Compara o conteúdo de texto dos elementos
        /// </summary>
        private static bool CompareTextContent(OpenXmlElement element1, OpenXmlElement element2, ComparisonOptions options)
        {
            // Comparação específica para elementos de texto
            if (element1 is Text text1 && element2 is Text text2)
            {
                return CompareTextValues(text1.Text, text2.Text, options);
            }

            // Para outros elementos que podem ter InnerText
            if (!element1.HasChildren && !element2.HasChildren)
            {
                return CompareTextValues(element1.InnerText, element2.InnerText, options);
            }

            return true;
        }

        /// <summary>
        /// Compara valores de texto
        /// </summary>
        private static bool CompareTextValues(string text1, string text2, ComparisonOptions options)
        {
            if (options.IgnoreWhitespace)
            {
                text1 = text1?.Trim();
                text2 = text2?.Trim();
            }

            if (options.IgnoreCase)
            {
                return string.Equals(text1, text2, StringComparison.OrdinalIgnoreCase);
            }

            return string.Equals(text1, text2);
        }

        /// <summary>
        /// Compara os elementos filhos
        /// </summary>
        private static bool CompareChildren(OpenXmlElement element1, OpenXmlElement element2, ComparisonOptions options)
        {
            var children1 = GetFilteredChildren(element1, options);
            var children2 = GetFilteredChildren(element2, options);

            if (children1.Count != children2.Count)
                return false;

            for (int i = 0; i < children1.Count; i++)
            {
                if (!CompareElements(children1[i], children2[i], options))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Obtém filhos filtrados baseado nas opções
        /// </summary>
        private static List<OpenXmlElement> GetFilteredChildren(OpenXmlElement element, ComparisonOptions options)
        {
            var children = element.ChildElements.ToList();

            if (options.IgnoreEmptyElements)
            {
                children = children.Where(c => !IsEmptyElement(c)).ToList();
            }

            if (options.IgnoreElementTypes?.Any() == true)
            {
                children = children.Where(c => !options.IgnoreElementTypes.Contains(c.GetType())).ToList();
            }

            return children;
        }

        /// <summary>
        /// Verifica se um elemento está vazio
        /// </summary>
        private static bool IsEmptyElement(OpenXmlElement element)
        {
            return !element.HasChildren &&
                   string.IsNullOrWhiteSpace(element.InnerText) &&
                   !element.GetAttributes().Any();
        }

        /// <summary>
        /// Compara dois elementos e retorna informações detalhadas sobre as diferenças
        /// </summary>
        /// <param name="element1">Primeiro elemento</param>
        /// <param name="element2">Segundo elemento</param>
        /// <param name="options">Opções de comparação</param>
        /// <returns>Resultado da comparação com detalhes</returns>
        public static ComparisonResult CompareDetailed(OpenXmlElement element1, OpenXmlElement element2, ComparisonOptions options = null)
        {
            options ??= new ComparisonOptions();
            var result = new ComparisonResult();

            CompareElementsDetailed(element1, element2, options, result, "");

            return result;
        }

        /// <summary>
        /// Comparação detalhada com coleta de diferenças
        /// </summary>
        private static void CompareElementsDetailed(OpenXmlElement element1, OpenXmlElement element2,
            ComparisonOptions options, ComparisonResult result, string path)
        {
            if (element1 == null && element2 == null)
                return;

            if (element1 == null)
            {
                result.AddDifference($"{path}: Elemento ausente no primeiro documento");
                return;
            }

            if (element2 == null)
            {
                result.AddDifference($"{path}: Elemento ausente no segundo documento");
                return;
            }

            var currentPath = string.IsNullOrEmpty(path) ? element1.LocalName : $"{path}/{element1.LocalName}";

            // Comparar tipos
            if (element1.GetType() != element2.GetType())
            {
                result.AddDifference($"{currentPath}: Tipos diferentes ({element1.GetType().Name} vs {element2.GetType().Name})");
                return;
            }

            // Comparar atributos
            CompareAttributesDetailed(element1, element2, options, result, currentPath);

            // Comparar conteúdo de texto
            CompareTextContentDetailed(element1, element2, options, result, currentPath);

            // Comparar filhos
            CompareChildrenDetailed(element1, element2, options, result, currentPath);
        }

        private static void CompareAttributesDetailed(OpenXmlElement element1, OpenXmlElement element2,
            ComparisonOptions options, ComparisonResult result, string path)
        {
            var attrs1 = GetFilteredAttributes(element1, options);
            var attrs2 = GetFilteredAttributes(element2, options);

            foreach (var attr1 in attrs1)
            {
                var attr2 = attrs2.FirstOrDefault(a => a.LocalName == attr1.LocalName);
                if (attr2 == null)
                {
                    result.AddDifference($"{path}@{attr1.LocalName}: Atributo ausente no segundo elemento");
                }
                else if (!CompareAttributeValues(attr1.Value, attr2.Value, options))
                {
                    result.AddDifference($"{path}@{attr1.LocalName}: Valores diferentes ('{attr1.Value}' vs '{attr2.Value}')");
                }
            }

            foreach (var attr2 in attrs2)
            {
                if (!attrs1.Any(a => a.LocalName == attr2.LocalName))
                {
                    result.AddDifference($"{path}@{attr2.LocalName}: Atributo ausente no primeiro elemento");
                }
            }
        }

        private static void CompareTextContentDetailed(OpenXmlElement element1, OpenXmlElement element2,
            ComparisonOptions options, ComparisonResult result, string path)
        {
            if (element1 is Text text1 && element2 is Text text2)
            {
                if (!CompareTextValues(text1.Text, text2.Text, options))
                {
                    result.AddDifference($"{path}: Texto diferente ('{text1.Text}' vs '{text2.Text}')");
                }
            }
        }

        private static void CompareChildrenDetailed(OpenXmlElement element1, OpenXmlElement element2,
            ComparisonOptions options, ComparisonResult result, string path)
        {
            var children1 = GetFilteredChildren(element1, options);
            var children2 = GetFilteredChildren(element2, options);

            int maxCount = Math.Max(children1.Count, children2.Count);

            for (int i = 0; i < maxCount; i++)
            {
                var child1 = i < children1.Count ? children1[i] : null;
                var child2 = i < children2.Count ? children2[i] : null;

                CompareElementsDetailed(child1, child2, options, result, path);
            }
        }
    }
}
