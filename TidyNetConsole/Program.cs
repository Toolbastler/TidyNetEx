using TidyNetConsole;

// including new tag <mark>
TryExec("<html><head><body>Das ist ein <mark>Täst</mark> mit Umlauten!</body>");


// fehlerhaft
TryExec("<html><head><body>Das ist ein <mark>Täst</mark> <notexists>mit</notexists> Umlauten!</body>");

System.Console.WriteLine("[Enter] zum Beenden");
Console.ReadLine();

void TryExec(string html)
{
    Console.WriteLine("Input:\r\n{0}", html);

    var tidiedHtml = TidyNetHelper.Tidy(html);

    Console.WriteLine("Output:\r\n{0}\r\n", tidiedHtml);
}