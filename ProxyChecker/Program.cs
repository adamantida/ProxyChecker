using ConsoleApp1.classer;
using ProxyChecker.classer;

namespace ProxyChecker;

internal static class Program
{
    private static void Main()
    {
        Console.Write("path: ");
        var path = Console.ReadLine() ?? string.Empty;

        var proxyParser = new ProxyParser(path ?? "proxy.txt");
        var proxyChecker = new classer.ProxyChecker();
        var proxyWriter = new ProxyWriter();
        proxyParser.ParseFromFile();
        Console.WriteLine(proxyParser.Count);

        for (var i = 0; i < proxyParser.Count; i++)
            if (proxyChecker.CheckProxyAsync(proxyParser[i]).Result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Good: {proxyParser[i]}");
                proxyWriter.Write(proxyParser[i] + "\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Bad: {proxyParser[i]}");
            }


        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(proxyChecker.Count);
        Console.ResetColor();
        Console.ReadKey();
    }
}