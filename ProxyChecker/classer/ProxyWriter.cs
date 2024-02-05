using System.Text;

namespace ConsoleApp1.classer;

public class ProxyWriter
{
    private Stream stream = new FileStream("good.txt", FileMode.Create);

    public void Write(string proxy)
    {
        stream.Write(Encoding.UTF8.GetBytes(proxy));
        stream.Flush();
    }
}