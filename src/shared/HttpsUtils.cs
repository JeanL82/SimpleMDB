using System.Buffers.Binary;
using System.Collections;
using System.Data.SqlTypes;
using System.Net;
using System.Text;
using System.Web;

namespace SimpleMDB
{
    public class HttpUtils
    {
        // Método para responder a las solicitudes HTTP de manera asincrónica
        public static async Task RespondAsync(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, int statusCode, string body)
        {
            byte[] content = Encoding.UTF8.GetBytes(body); // Convertir el cuerpo a bytes en UTF-8

            res.StatusCode = statusCode;
            res.ContentEncoding = Encoding.UTF8;
            res.ContentType = "text/html";
            res.ContentLength64 = content.LongLength;


            await res.OutputStream.WriteAsync(content); // Escribir los bytes en la salida
            res.Close(); // 
            
        }

        public static  async Task Redirect(HttpListenerRequest req, HttpListenerResponse res, Hashtable options,string location)
        {
            string message =(string ?)options["message"]?? "";
            string query = string.IsNullOrWhiteSpace(message)? "" : "?message=" + HttpUtils.UrlEncode(message);
            
            res.Redirect(location + query);
            res.Close();

            await Task.CompletedTask ;
        }

        private static string UrlEncode(string message)
        {
            throw new NotImplementedException();
        }

        public static async Task ReadRequestFormData(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
        {
            string type = req.ContentType?? "";
            if ( type.StartsWith("application/x-www-Form-urlencoded"))
            {
                 using var sr = new StreamReader(req.InputStream,Encoding.UTF8);
                 string body =await sr.ReadToEndAsync();
                 var formData = HttpUtility.ParseQueryString(body);


                 options["req.form"] = formData;


            }
        }

        internal static async Task Respond(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, int oK, string content)
        {
            throw new NotImplementedException();
        }
    }
}
        
    
