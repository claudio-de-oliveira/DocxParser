using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TemplateBuilder
{
    public class DocxTemplate
    {
        public static void ProcessDocx(MemoryStream stream, string jsonFile)
        {
            using WordprocessingDocument mainDocument = WordprocessingDocument.Open(stream, true);
            //var dictionary = Dictionary.ReadFromDocx(mainDocument)
            //    ?? throw new Exception($"O arquivo é inválido ou não contém um dicionário.");

            MainDocumentPart? mainDocumentPart = mainDocument.MainDocumentPart;

            string text = File.ReadAllText(jsonFile);

            //JsonNode json = JsonNode.Parse(text)!
            //    ?? throw new Exception($"O arquivo \"{jsonFile}\" é inválido.");
            //Response? _response = Response.Create(json);
            //if (_response is null || _response.Data is null || _response.Data.QuestionnaireJSON is null)
            //    throw new Exception($"O arquivo \"{jsonFile}\" não contém as informações requeridas.");

            Dictionary<string, ConcreteLL.Data.Variable> variables = [];
            //foreach (var key in _response.Data.QuestionnaireJSON.Variables.Dictionary.Keys)
            //    variables.Add(key, ConvertToLLVariable(_response.Data.QuestionnaireJSON.Variables.Dictionary[key]));

            var root = Utilities.TextSplitter.SplitCommandMarks(mainDocumentPart!.Document.Body!);

            mainDocumentPart!.Document.AddNamespaceDeclaration("s9", "http://schemas.idealsoft.com/shop9/2025/expression");

            var parserDocx = new ParserDocx(variables);

            var newBody = parserDocx.Parse(root);

            mainDocumentPart!.Document.Body = (Body)newBody!;

            mainDocument.Save();
        }

        private static ConcreteLL.Data.Variable ConvertToLLVariable(JsonElement.Variable variable)
            => new()
            {
                InputMethod = variable.InputMethod,
                Name = variable.Name,
                DataType = variable.DataType,
                FieldOnly = variable.FieldOnly,
                OccursOrder = variable.OccursOrder,
                Prompt = variable.Prompt,
                Selections = variable.Selections,
                Repeat = variable.Repeat,
                Repeats = variable.Repeats,
                Definition = variable.Definition,
                Logic = variable.Logic,
                DefaultFormat = variable.DefaultFormat,
                OriginalPrompt = variable.OriginalPrompt,
                Depth = variable.Depth,
                Relevant = variable.Relevant,
                Visible = variable.Visible,
                Value = variable.Value,
            };
    }
}
