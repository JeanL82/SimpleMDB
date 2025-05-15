using System.Collections;
using System.Net;
using System.Text;

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
            res.Close(); // Cerrar la respuesta
        }

        // Método no implementado, que lanza una excepción para indicar que aún no se ha implementado
        internal static void Respond(HttpListenerRequest req, HttpListenerResponse res, FileOptions fileOptions, string html)
        {
            throw new NotImplementedException();
        }

        // Método no implementado, lanza una excepción si se utiliza
        internal static void Respond(HttpListenerRequest req, HttpListenerResponse res, object options, string html)
        {
            throw new NotImplementedException();
        }

        // Método asincrónico para responder utilizando una tabla hash (Hashtable) de opciones
        internal static async Task Respond(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, int statusCode, int ok, string html)
        {
            try
            {
                // Verificar si 'options' no es nulo y contiene la clave 'body'
                if (options != null && options.ContainsKey("body"))
                {
                    var body = options["body"]?.ToString(); // Usar el operador null-conditional para evitar nulidad
                    if (body != null)
                    {
                        await RespondAsync(req, res, options, statusCode, body); // Llamar a RespondAsync con el cuerpo válido
                    }
                    else
                    {
                        // Si el valor es nulo, responder con un cuerpo vacío
                        await RespondAsync(req, res, options, statusCode, string.Empty);
                    }
                }
                else
                {
                    // Si no se encuentran las opciones, responder con el valor de html
                    await RespondAsync(req, res, options, statusCode, html);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, se responde con un código de error 500
                Console.WriteLine("Error in responding: " + ex.Message);
                res.StatusCode = 500; // Internal Server Error
                res.Close();
            }
        }

        internal static async Task Respond(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, int internalServerError, string v)
        {
            throw new NotImplementedException();
        }
    }
    
}