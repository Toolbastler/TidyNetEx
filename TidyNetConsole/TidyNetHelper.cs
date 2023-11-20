using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidyNet;

namespace TidyNetConsole;

internal class TidyNetHelper
{
    public string Tidy(string html)
    {
        var tidy = new Tidy();

        BuildOptions(tidy);

        var messages = new TidyMessageCollection();

        using (var input = new MemoryStream(Encoding.UTF8.GetBytes(html)))
        using (var output = new MemoryStream())
        {
            input.Position = 0;

            try
            {
                tidy.Parse(input, output, messages);

                output.Position = 0;

                return Encoding.UTF8.GetString(output.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Tidy'ing html failed: {0}", ex.Message);
            }

            return null;
        }
    }

    void BuildOptions(Tidy tidy)
    {
        /* tidy-optionen zur nachbearbeitung von html-text nach dem xslt-extraktor */
        /* Version 2: mit deklaration von html5-elementen */

        /* optionen zum erzeugen einer sauberen und flachen absatzstruktur */
        tidy.Options.EncloseBlockText = true;
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
        // forceoutput: yes
        tidy.Options.DocType = DocType.Omit;
        tidy.Options.MakeClean = false;
        // show-warnings: false
        // input-encoding: utf8
        // output-encoding: utf8
        tidy.Options.CharEncoding = CharEncoding.UTF8;
        
        // outputbom: yes

        /* html5 - Elemente deklarieren */
        tidy.Options.NewBlocklevelTags = "aside article figure figcaption";
        tidy.Options.NewInlineTags = "mark";
    }
}