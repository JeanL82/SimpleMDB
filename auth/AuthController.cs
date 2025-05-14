using System.Net;
using System.Text;
using System.Collections;

namespace SimpleMDB;

public class LandingController // nuevo nombre
{
    public LandingController()
    {
    }

    public async Task LandingPage(HttpListenerRequest req, HttpListenerResponse res, Hashtable option)
    {
        string html = HtmlTemplate.Base("SimpleMDB", "Landing Page", "Hello, world!");
        byte[] content = Encoding.UTF8.GetBytes(html);

        res.StatusCode = (int)HttpStatusCode.OK;
        res.ContentEncoding = Encoding.UTF8;
        res.ContentType = "text/html";
        res.ContentLength64 = content.LongLength;

        await res.OutputStream.WriteAsync(content, 0, content.Length);
        res.Close();
    }
}