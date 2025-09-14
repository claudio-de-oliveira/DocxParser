using TemplateBuilder;

if (args.Length != 3)
{
    Console.WriteLine("Builder <template.docx> <source.json> <target.docx>");
    return;
}

try
{
    byte[] byteArray = File.ReadAllBytes(args[0]);

    using (var stream = new MemoryStream())
    {
        stream.Write(byteArray, 0, (int)byteArray.Length);

        DocxTemplate.ProcessDocx(stream, args[1]);

        File.WriteAllBytes(args[2], stream.ToArray());

        stream.Dispose();
    }

    Console.WriteLine($"O documento {args[2]} foi criado com sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
