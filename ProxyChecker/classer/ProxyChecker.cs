using System.Net;
using System.Net.Sockets;

namespace ProxyChecker.classer;

public class ProxyChecker
{
    private List<string> _proxyGood = new();


    public async Task<bool> CheckProxyAsync(string proxy)
    {
        string[] proxyParts = proxy.Split(':');
        using var client = new TcpClient();
        try
        {
            if (!await CheckedOnMeToYouAsync(500) || !await CheckForInternetConnectionAsync(500)) return false;

            AddProxy(proxy);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

        async Task<bool> CheckForInternetConnectionAsync(int timeOut = 3000)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://google.com/");
                httpWebRequest.Method = "HEAD";
                httpWebRequest.Timeout = timeOut;
                httpWebRequest.Proxy = new WebProxy(proxyParts[0], int.Parse(proxyParts[1]));

                using (var response = await httpWebRequest.GetResponseAsync())
                using (response.GetResponseStream())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        async Task<bool> CheckedOnMeToYouAsync(int timeOut = 3000)
        {
            var tmp = client.BeginConnect(proxyParts[0], int.Parse(proxyParts[1]), null, null);
            return await Task.Run(() => tmp.AsyncWaitHandle.WaitOne(timeOut));
        }
    }


    public void AddProxy(string proxy)
    {
        _proxyGood.Add(proxy);
    }

    public List<string> GetList()
    {
        return _proxyGood;
    }

    public int Count => _proxyGood.Count;
}