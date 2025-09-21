
using DocumentFormat.OpenXml.Wordprocessing;

using Utilities;

namespace TestParser
{
    public class TestUtilities
    {
        [Fact]
        public void ComparacaoSimples()
        {
            var para1 = new Paragraph();
            para1.AppendChild(new Run(new Text("Olá mundo")));

            var para2 = new Paragraph();
            para2.AppendChild(new Run(new Text("Olá mundo")));

            bool saoIguais = OpenXmlEqualityComparer.AreEqual(para1, para2);
            Assert.True(saoIguais);
        }

        [Fact]
        public void ComparacaoComOpcoes()
        {
            var options = new ComparisonOptions
            {
                IgnoreCase = true,
                IgnoreWhitespace = true
            };

            var text1 = new Text("  TEXTO  ");
            var text2 = new Text("texto");

            bool saoIguaisComOpcoes = OpenXmlEqualityComparer.AreEqual(text1, text2, options);
            Assert.True(saoIguaisComOpcoes);
        }

        [Fact]
        public void ComparacaoDetalhadaDiferentes()
        {
            var run1 = new Run
            {
                RunProperties = new RunProperties(new Bold())
            };
            run1.AppendChild(new Text("Texto em negrito"));

            var run2 = new Run
            {
                RunProperties = new RunProperties(new Italic())
            };
            run2.AppendChild(new Text("Texto em itálico"));

            var resultado = OpenXmlEqualityComparer.CompareDetailed(run1, run2);
            Assert.False(resultado.AreEqual);
        }

        [Fact]
        public void ComparacaoDetalhadaIguais()
        {
            var run1 = new Run() { RunProperties = new RunProperties() };
            run1.RunProperties.AppendChild(new Italic());
            run1.RunProperties.AppendChild(new Bold());
            run1.AppendChild(new Text("Texto em negrito e itálico"));

            var run2 = new Run() { RunProperties = new RunProperties() };
            run2.RunProperties.AppendChild(new Italic());
            run2.RunProperties.AppendChild(new Bold());
            run2.AppendChild(new Text("Texto em negrito e itálico"));

            var resultado = OpenXmlEqualityComparer.CompareDetailed(run1, run2);
            Assert.True(resultado.AreEqual);
        }

        [Fact]
        public void ComparacaoIgnorandoTiposEspecificosDiferentes()
        {
            var run1 = new Run();
            run1.RunProperties = new RunProperties(new Bold());
            run1.AppendChild(new Text("Texto em negrito"));

            var run2 = new Run();
            run2.RunProperties = new RunProperties(new Italic());
            run2.AppendChild(new Text("Texto em itálico"));

            var optionsIgnoreFormatting = new ComparisonOptions();
            optionsIgnoreFormatting.IgnoreElementTypes.Add(typeof(RunProperties));

            bool ignorandoFormatacao = OpenXmlEqualityComparer.AreEqual(run1, run2, optionsIgnoreFormatting);
            Assert.False(ignorandoFormatacao);
        }

        [Fact]
        public void ComparacaoIgnorandoTiposEspecificosIguais()
        {
            var run1 = new Run();
            run1.RunProperties = new RunProperties(new Bold());
            run1.AppendChild(new Text("Texto em negrito"));

            var run2 = new Run();
            run2.RunProperties = new RunProperties(new Bold());
            run2.AppendChild(new Text("Texto em negrito"));

            var optionsIgnoreFormatting = new ComparisonOptions();
            optionsIgnoreFormatting.IgnoreElementTypes.Add(typeof(RunProperties));

            bool ignorandoFormatacao = OpenXmlEqualityComparer.AreEqual(run1, run2, optionsIgnoreFormatting);
            Assert.True(ignorandoFormatacao);
        }

        [Fact]
        public void ConversaoParagrafoSimples()
        {
            string xmlParagrafo = @"<w:p xmlns:w='http://schemas.openxmlformats.org/wordprocessingml/2006/main'>
                                    <w:r>
                                        <w:t>Texto de exemplo</w:t>
                                    </w:r>
                                 </w:p>";

            var paragrafo = OpenXmlConverter.ConvertInnerXmlToElement<Paragraph>(xmlParagrafo);
            Assert.Equal("Paragraph", paragrafo.GetType().Name);
            Assert.Equal("Run", paragrafo.FirstChild?.GetType().Name);
            Assert.Equal("Text", paragrafo.FirstChild?.FirstChild?.GetType().Name);
        }

        [Fact]
        public void ConversaoRunComFormatacao()
        {
            string xmlRun = @"<w:r xmlns:w='http://schemas.openxmlformats.org/wordprocessingml/2006/main'>
                             <w:rPr>
                                 <w:b/>
                                 <w:i/>
                             </w:rPr>
                             <w:t>Texto em negrito e itálico</w:t>
                         </w:r>";

            var run = OpenXmlConverter.ConvertInnerXmlToElement<Run>(xmlRun);
            Assert.Equal("Run", run.GetType().Name);
            Assert.Equal("RunProperties", run.RunProperties?.GetType().Name);
            Assert.Equal("Bold", run.RunProperties?.Bold?.GetType().Name);
            Assert.Equal("Italic", run.RunProperties?.Italic?.GetType().Name);
            Assert.Equal("Text", run.LastChild?.GetType().Name);
        }

        [Fact]
        public void ConversaoUsandoMetodoGenerico()
        {
            string xmlTexto = @"<w:t xmlns:w='http://schemas.openxmlformats.org/wordprocessingml/2006/main'>Texto simples</w:t>";

            var elemento = OpenXmlConverter.ConvertInnerXmlToElement(xmlTexto);
            Console.WriteLine($"\nElemento texto criado: {elemento.GetType().Name}");
            Assert.Equal(((Text)elemento).Text, "Texto simples");
        }

        [Fact]
        public void ConversaoTabelaSimples()
        {
            string xmlTabela = @"<w:tbl xmlns:w='http://schemas.openxmlformats.org/wordprocessingml/2006/main'>
                                <w:tr>
                                    <w:tc>
                                        <w:p>
                                            <w:r>
                                                <w:t>Célula 1</w:t>
                                            </w:r>
                                        </w:p>
                                    </w:tc>
                                </w:tr>
                             </w:tbl>";

            var tabela = OpenXmlConverter.ConvertInnerXmlToElement<Table>(xmlTabela);
            Assert.Equal("Table", tabela.GetType().Name);
            var row = tabela.FirstChild;
            Assert.Equal("TableRow", row?.GetType().Name);
            var cell = row?.FirstChild;
            Assert.Equal("TableCell", cell?.GetType().Name);
        }
    }
}