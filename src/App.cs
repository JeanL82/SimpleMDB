
using System.Collections;
using System.Net;
using System.Text;


namespace SimpleMDB
{
    public class App
    {
        private HttpListener server;
        private Httpsrouter router;

        public App()
        {
             var host = "http://127.0.0.1:8085/";
    server = new HttpListener();
    server.Prefixes.Add(host);
    Console.WriteLine("Server listening on " + host);

    var userRepository = new MockUser();
    var userService = new MockUser(userRepository);
    var authController = new AuthController();
    var usersController = new UserController(userService);
    router = new Httpsrouter();

    router.AddGet("/", authController.LandingPageGet);
    router.AddGet("/users", usersController.ViewAllGet);
}

        public async Task Start()
        {
            server.Start();
            while (server.IsListening)
            {
                var ctx = await server.GetContextAsync();
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
            var request = ctx.Request;
            var response = ctx.Response;
            var options = new Hashtable();

            await router.Handle(request, response, options);
        }
    }

    internal class MockUserService
    {
        private MockUser userRepository;

        public MockUserService(MockUser userRepository)
        {
            this.userRepository = userRepository;
        }
    }

    internal class AuthController
    {
        // Propiedad pública que retorna un handler para la ruta "/"
        public Func<HttpListenerRequest, HttpListenerResponse, Hashtable, Task> LandingPageGet =>
            async (request, response, options) =>
            {
                string responseString = "<html><body><h1>Welcome to SimpleMDB!</h1></body></html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";

                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            };
    }

    internal class Httpsrouter
    {
        private readonly Dictionary<string, Func<HttpListenerRequest, HttpListenerResponse, Hashtable, Task>> getRoutes =
            new();

        public void AddGet(string path, Func<HttpListenerRequest, HttpListenerResponse, Hashtable, Task> handler)
        {
            getRoutes[path] = handler;
        }

        public async Task Handle(HttpListenerRequest request, HttpListenerResponse response, Hashtable options)
        {
            if (request.HttpMethod == "GET" && getRoutes.TryGetValue(request.RawUrl, out var handler))
            {
                await handler(request, response, options);
            }
            else
            {
                response.StatusCode = 404;
                byte[] buffer = Encoding.UTF8.GetBytes("404 Not Found");
                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }

        internal void AddGet(string v, object viewAllGet)
        {
            throw new NotImplementedException();
        }
    }
}