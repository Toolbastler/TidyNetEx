using TidyNetConsole;

var html = File.ReadAllText(@"C:\Development\htmlcontentextractor\Source\HtmlContentExtractor\bin\x64\Debug\input.html");

var result = TryExec(html);

// including new tag <mark>
TryExec("<html><head><body>Das ist ein <mark>Täst</mark> mit Umlauten!</body>");

// fehlerhaft
TryExec("<html><head><body>Das ist ein <mark>Täst</mark> <notexists>mit</notexists> Umlauten!</body>");


Console.WriteLine("[Enter] zum Beenden");
Console.ReadLine();

string? TryExec(string html)
{
    Console.WriteLine("Input:\r\n{0}", html);

    var tidiedHtml = TidyNetHelper.Tidy(html);

    Console.WriteLine("Output:\r\n{0}\r\n", tidiedHtml);

    Console.WriteLine("-----------------------------------------------------");

    return tidiedHtml;
}