using System.Net;
using System.Collections;


namespace SimpleMDB
{
    public class LandingController // nuevo nombre
    {
        public LandingController()
        {
        }

        public async Task LandingPage(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
        {
            string html = HtmlTemplate.Base("SimpleMDB", "Landing Page", "Hello, world!");
            await HttpUtils.Respond(req, res, options, 200,(int)HttpStatusCode.OK,html);
        }
    }
}