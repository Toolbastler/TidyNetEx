using TidyNetConsole;

while (true)
{
    var html = File.ReadAllText(@"TestData\test.html");

    var result = TryExec(html);
    
//// including new tag <mark>
//TryExec("<html><head><body>Das ist ein <mark>Täst</mark> mit Umlauten!</body>");

//// fehlerhaft
//TryExec("<html><head><body>Das ist ein <mark>Täst</mark> <notexists>mit</notexists> Umlauten!</body>");

    Console.WriteLine("[Enter] zum Neustarten");
    Console.ReadLine();
}

string? TryExec(string html)
{
    Console.WriteLine("Input:\r\n{0}", html);

    var tidiedHtml = new TidyNetHelper { ShowInfos = false, DropBRElements = true }.Tidy(html);

    Console.WriteLine("Output:\r\n{0}\r\n", tidiedHtml);

    Console.WriteLine("-----------------------------------------------------");

    return tidiedHtml;
}