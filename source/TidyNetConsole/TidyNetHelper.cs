using System.Diagnostics;
using System.Text;
using TidyNet;

namespace TidyNetConsole;

[DebuggerStepThrough]
internal static class TidyNetHelper
{
    public static string Tidy(string html)
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
            if (tidy.Options.ShowInfos)
                Console.WriteLine($"Errors: {messages.Errors} | Warnings: {messages.Warnings}");

            foreach (var message in messages)
            {
                if (tidy.Options.ShowInfos && message.Level == MessageLevel.Info ||
                    tidy.Options.ShowWarnings && message.Level == MessageLevel.Warning)
                {
                    Console.WriteLine(message);
                }
            }
        }

        if (messages.Errors > 0 && output.Length == 0)
            throw new InvalidOperationException($"{messages.Errors} errors occurred: " +
                                                string.Join(" | ", messages.Where(m => m.Level == MessageLevel.Error)));

        var result = Encoding.UTF8.GetString(output.ToArray());

        if (tidy.Options.DropBRElements)
        {
            result = result.Replace("\r\n<br />", "").Replace("\n<br />", "").Replace("\r<br />", "");
            result = result.Replace("\r\n<br>", "").Replace("\n<br>", "").Replace("\r<br>", "");
        }

        result = result.Replace("\r\n\r\n", "\r\n").Replace("\n\n", "\n").Replace("\r\r", "\r");
        
        return result;
    }
    
    static void BuildOptions(Tidy tidy)
    {
        /* optionen zum erzeugen einer sauberen und flachen absatzstruktur */
        //tidy.Options.EncloseBlockText = true;
        tidy.Options.EncloseText = true;
        tidy.Options.DropEmptyParas = true;
        tidy.Options.DropEmptyElements = true;

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
        tidy.Options.WrapLen = 0;

        /* html-entities in xml-faehige zeichenreferenzen umwandeln */
        tidy.Options.NumEntities = true;
        tidy.Options.Xhtml = true;
        tidy.Options.XmlOut = false;
        tidy.Options.XmlPi = false;
        tidy.Options.XmlPIs = true;
        tidy.Options.XmlSpace = false;


        /* allgemeine optionen */
        tidy.Options.DocType = DocType.Omit;
        tidy.Options.MakeClean = true;
        tidy.Options.CharEncoding = CharEncoding.UTF8;
        
        /* html5 - Elemente deklarieren */
        tidy.Options.NewBlocklevelTags = "aside article figure figcaption";
        tidy.Options.NewInlineTags = "mark";

        tidy.Options.ShowInfos = false;
        tidy.Options.ShowWarnings = false;

        tidy.Options.DropBRElements = true;
    }
}