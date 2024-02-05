using System.Text;
using System.Text.RegularExpressions;

namespace ProxyChecker.classer;

public class ProxyParser
{
    private readonly string _path;
    public readonly List<string> List = new();

    public ProxyParser(string path)
    {
        _path = path;
    }

    public string this[int index] => List[index];

    public void ParseFromFile()
    {
        var fs = new FileStream(_path, FileMode.Open);
        var streamReader = new StreamReader(fs, Encoding.UTF8, true, 10000);

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine() ?? "";
            var tmp = Regex.Match(line,
                    @"\b(?:(?:2(?:[0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9])\.){3}(?:(?:2([0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9]))\b:[0-9]+")
                .ToString();
            if (tmp != "") List.Add(tmp);
        }
    }

    public List<string> GetList()
    {
        return List;
    }

    public int Count => List.Count;
}