using System;
using System.Net;
using System.Threading.Tasks;

public class Listener
{
    public static void Main()
    {
        string prefix = "http://localhost:8888/";
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(prefix);
        listener.Start();
        Console.WriteLine("Listening...");
        Task.Run(() => StartListener(listener));
        Console.ReadLine();
    }

    public static async void StartListener(HttpListener listener)
    {
        while (true)
        {
            HttpListenerContext context = await listener.GetContextAsync();
            await Task.Run(() => ProcessRequest(context));
        }
    }

    public static void ProcessRequest(HttpListenerContext context)
    {
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
                responseString = "Client Error!";
                break;
            case "/ServerError/":
                response.StatusCode = 500;
                responseString = "Server Error!";
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