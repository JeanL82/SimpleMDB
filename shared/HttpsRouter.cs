using System.Net;
using System.Collections;

namespace SimpleMDB;

public class HttpRouter
{
    public static readonly int Response_NOT_SENT_YET = 777;
    private List<HttpMiddleware> middlewares;
    private List<(string,string,HttpMiddleware[] middlewares) > endpoint;

    public HttpRouter()
    {
        middlewares = [];
        endpoint = [];
    }

    public void Use(params HttpMiddleware[] middlewares)
    {
        this.middlewares.AddRange(middlewares);
    }

    public void AddEndpoint(string method, string route, params HttpMiddleware[] middleware)
    {
        this.endpoint.Add((method ,route, middleware));


    }

    public void AddGet(string route , params HttpMiddleware[]middlewares)
    {
        AddEndpoint("Get", route,middlewares);

    }
      public void AddPost(string route , params HttpMiddleware[]middlewares)
    {
        AddEndpoint("Post", route,middlewares);

    }
      public void AddPut(string route , params HttpMiddleware[]middlewares)
    {
        AddEndpoint("Put", route,middlewares);

    }
      public void AddDelete(string route , params HttpMiddleware[]middlewares)
    {
        AddEndpoint("Delete", route,middlewares);

    }

    public async Task Handle(HttpListenerRequest req,HttpListenerResponse res, Hashtable option)
    {
        res.StatusCode = Response_NOT_SENT_YET;

        foreach(var middleware in middlewares)
        {
            await middleware(req,res,option);
            if(res.StatusCode != Response_NOT_SENT_YET){return;}
        }
        
       foreach (var (method, route, endpointMiddlewares) in endpoint)
        {
            if (req.HttpMethod.ToUpper() == method.ToUpper() && req.Url!.AbsolutePath == route)
            {
                foreach (var middleware in endpointMiddlewares)
                {
                    await middleware(req, res, option);
                    if (res.StatusCode != Response_NOT_SENT_YET) return;
                }
            }
        }
    
    }
}