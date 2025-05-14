using System.Collections;
using System.Net;
using System.Text;

namespace SimpleMDB
{
    public class HttpUtils
    {
        public static async Task RespondAsync(HttpListenerRequest req, HttpListenerResponse res, System.Collections.Hashtable options, int statusCode, string body)
        {
            byte[] content = Encoding.UTF8.GetBytes(body); // Corregido el punto y coma

            res.StatusCode = statusCode;
            res.ContentEncoding = Encoding.UTF8;
            res.ContentType = "text/html";
            res.ContentLength64 = content.LongLength;
         await res.OutputStream.WriteAsync(content);
            res.Close();
        }

        internal static async Task Respond(HttpListenerRequest req, HttpListenerResponse res, FileOptions fileOptions, string html)
        {
            throw new NotImplementedException();
        }

        internal static async Task Respond(HttpListenerRequest req, HttpListenerResponse res, object options, string html)
        {
            throw new NotImplementedException();
        }

        internal static async Task Respond(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, int v, int oK, string html)
        {
            throw new NotImplementedException();
        }
    }
}
