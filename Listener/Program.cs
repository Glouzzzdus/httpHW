using System;
using System.Net;

public class Listener
{
    public static void Main()
    {
        string prefix = "http://localhost:8888/";
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(prefix);
        listener.Start();
        Console.WriteLine("Listening...");

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string responseString;

            switch (request.Url.AbsolutePath)
            {
                case "/Information/":
                    response.StatusCode = 100;
                    responseString = "Information!";
                    break;
                case "/Success/":
                    response.StatusCode = 200;
                    responseString = "Success!";
                    break;
                case "/Redirection/":
                    response.StatusCode = 300;
                    responseString = "Redirection!";
                    break;
                case "/ClientError/":
                    response.StatusCode = 400;
                    responseString = "ClientError!";
                    break;
                case "/ServerError/":
                    response.StatusCode = 500;
                    responseString = "ServerError!";
                    break;
                case "/MyNameByHeader/":
                    response.StatusCode = 200;
                    response.Headers.Add("X-MyName", "YourName");
                    responseString = "Name is added in the header!";
                    break;
                case "/MyNameByCookies/":
                    response.StatusCode = 200;
                    response.Cookies.Add(new Cookie("MyName", "YourName"));
                    responseString = "Cookie has been set!";
                    break;
                default:
                    response.StatusCode = 404;
                    responseString = "Not Found!";
                    break;
            }

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}