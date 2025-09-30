using DocumentFormat.OpenXml.Wordprocessing;

using TemplateBuilder;

using Utilities;

namespace TestParser
{
    public class TestNestingBooleanExpressions
    {
        private static Dictionary<string, ConcreteLL.Data.Variable> variables = Variables.GetVariables();

        [Fact]
        public void TestNestingFalseExp()
        {
            var xmlText = """
                <w:p xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
                    <w:pPr>
                        <w:rPr><w:color w:val="006400" /></w:rPr>
                    </w:pPr>
                    <w:r>
                        <w:rPr><w:color w:val="006400" /></w:rPr>
                        <w:t>&lt;</w:t>
                    </w:r>
                    <w:r><w:rPr><w:color w:val="ff0000" /><w:vertAlign w:val="superscript" /></w:rPr><w:t>False</w:t></w:r>
                    <w:r><w:rPr><w:color w:val="006400" /></w:rPr><w:t>&lt;</w:t></w:r>
                    <w:r><w:rPr><w:color w:val="ff0000" /><w:vertAlign w:val="superscript" /></w:rPr><w:t>False</w:t></w:r>
                    <w:r><w:rPr><w:color w:val="006400" /></w:rPr><w:t>&lt;</w:t></w:r>
                    <w:r><w:rPr><w:color w:val="ff0000" /><w:vertAlign w:val="superscript" /></w:rPr><w:t>False</w:t></w:r>
                    <w:r><w:t>Break</w:t></w:r>
                    <w:r><w:rPr><w:color w:val="006400" /></w:rPr><w:t>3&gt;</w:t></w:r>
                    <w:r><w:t>Break</w:t></w:r>
                    <w:r><w:rPr><w:color w:val="006400" /></w:rPr><w:t>2&gt;</w:t></w:r>
                    <w:r><w:t>Break</w:t></w:r>
                    <w:r><w:rPr><w:color w:val="006400" /></w:rPr><w:t>1&gt;</w:t></w:r>
                </w:p>
                """;

            //var xmlElement = OpenXmlConverter.ConvertInnerXmlToElement<Paragraph>(xmlText);
            var xmlElement = OpenXmlConverter.ConvertInnerXmlToElement(xmlText);

            var body = new Body();
            body.AppendChild(xmlElement);
            body = Utilities.TextSplitter.SplitCommandMarks(body) as Body;
            var scannerDocx = new ScannerDocx(variables, body!);



            var context = scannerDocx.ScanXmlElementInterno(body.CloneNode(true));

        }
    }
}
