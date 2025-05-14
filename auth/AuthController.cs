using System.Net;
using System.Text;
using System.Collections;

namespace SimpleMDB;

public static class HtmlResponseHelper
{
    public static async Task RespondAsync(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, int statusCode, string body)
    {
        byte[] content = Encoding.UTF8.GetBytes(body);

        res.StatusCode = statusCode;
        res.ContentEncoding = Encoding.UTF8;
        res.ContentType = "text/html";
        res.ContentLength64 = content.LongLength;

        await res.OutputStream.WriteAsync(content, 0, content.Length);
        res.Close();
    }

    public static Task RespondNotFound(HttpListenerResponse res)
    {
        string body = "<html><body><h1>404 Not Found</h1></body></html>";
        return RespondAsync(null!, res, null!, 404, body);
    }

    public static Task RespondServerError(HttpListenerResponse res)
    {
        string body = "<html><body><h1>500 Internal Server Error</h1></body></html>";
        return RespondAsync(null!, res, null!, 500, body);
    }

    public static Task RespondBadRequest(HttpListenerResponse res)
    {
        string body = "<html><body><h1>400 Bad Request</h1></body></html>";
        return RespondAsync(null!, res, null!, 400, body);
    }
}