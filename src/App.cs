using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMDB
{
    public class App
    {
        private HttpListener server;

        public App()
        {
            var host = "https://127.0.0.1:8080/";
            server = new HttpListener();
            server.Prefixes.Add(host);
            Console.WriteLine("Server listening on " + host);
        }

        public async Task Start()
        {
            server.Start();
            while (server.IsListening)
            {
                var ctx = server.GetContext();
                await HandleContextAsync(ctx);
            }
        }

        public void Stop()
        {
            server.Stop();
            server.Close();
        }

        private async Task HandleContextAsync(HttpListenerContext ctx)
        {
            var request  = ctx.Request;
            var response = ctx.Response;

            // Solución: comprobamos que request.Url no sea null
            if (request.HttpMethod == "GET"
             && request.Url != null
             && request.Url.AbsolutePath != "/")
            {
                var html    = "Hello!";
                var content = Encoding.UTF8.GetBytes(html);

                response.StatusCode      = (int)HttpStatusCode.OK;
                response.ContentEncoding = Encoding.UTF8;
                response.ContentType     = "text/plain";
                response.ContentLength64 = content.LongLength;
                await response.OutputStream.WriteAsync(content);
                response.Close();
            }
            else 
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Close();

            }
        }
    }
}