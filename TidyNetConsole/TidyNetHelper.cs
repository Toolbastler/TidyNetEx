using System.Text;
using TidyNet;

namespace TidyNetConsole;

internal static class TidyNetHelper
{
    public static string? Tidy(string html)
    {
        var tidy = new Tidy();

        BuildOptions(tidy);

        var messages = new TidyMessageCollection();

        using var input = new MemoryStream(Encoding.UTF8.GetBytes(html));
        using var output = new MemoryStream();

        input.Position = 0;

        tidy.Parse(input, output, messages);

        output.Position = 0;

        if (messages.Any())
        {
            Console.WriteLine($"Errors: {messages.Errors} | Warnings: {messages.Warnings}");
            messages.ForEach(Console.WriteLine);
        }

        if (messages.Errors > 0 && output.Length == 0)
            throw new InvalidOperationException($"{messages.Errors} errors occurred: " +
                                                string.Join(" | ", messages.Where(m => m.Level == MessageLevel.Error)));

        return Encoding.UTF8.GetString(output.ToArray());
    }

    static void BuildOptions(Tidy tidy)
    {
        /* optionen zum erzeugen einer sauberen und flachen absatzstruktur */
        // tidy.Options.EncloseBlockText = true;
        tidy.Options.EncloseText = true;
        tidy.Options.DropEmptyParas = true;

        /* keinen whitespace hinzufuegen */
        tidy.Options.BreakBeforeBR = false;
        tidy.Options.IndentAttributes = false;
        tidy.Options.IndentContent = false;
        tidy.Options.SmartIndent = false;
        tidy.Options.WrapAsp = false;
        tidy.Options.WrapAttVals = false;
        tidy.Options.WrapJste = false;
        tidy.Options.WrapPhp = false;
        tidy.Options.WrapScriptlets = false;
        tidy.Options.WrapSection = false;

        /* html-entities in xml-faehige zeichenreferenzen umwandeln */
        tidy.Options.NumEntities = true;
        tidy.Options.Xhtml = true;
        tidy.Options.XmlOut = false;
        tidy.Options.XmlPi = false;
        tidy.Options.XmlSpace = false;

        /* allgemeine optionen */
        tidy.Options.DocType = DocType.Omit;
        tidy.Options.MakeClean = false;
        tidy.Options.CharEncoding = CharEncoding.UTF8;
        
        /* html5 - Elemente deklarieren */
        tidy.Options.NewBlocklevelTags = "aside article figure figcaption";
        tidy.Options.NewInlineTags = "mark";
    }
}