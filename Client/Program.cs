using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

class Client
{
    static readonly HttpClientHandler handler = new HttpClientHandler() { UseCookies = true };
    static readonly HttpClient client = new HttpClient(handler);

    static async Task Main()
    {
        string[] urls = new[]
        {
            "http://localhost:8888/Information/",
            "http://localhost:8888/Success/",
            "http://localhost:8888/Redirection/",
            "http://localhost:8888/ClientError/",
            "http://localhost:8888/ServerError/",
            "http://localhost:8888/MyNameByHeader/",
            "http://localhost:8888/MyNameByCookies/"
        };

        foreach (var url in urls)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(url + " " + (int)response.StatusCode + " " + response.StatusCode);
                var myNameHeader = response.Headers.FirstOrDefault(x => x.Key == "X-MyName");
                if (myNameHeader.Value != null)
                {
                    Console.WriteLine("My Name is: " + myNameHeader.Value.FirstOrDefault());
                }

                var myNameCookie = handler.CookieContainer.GetCookies(new Uri(url)).Cast<System.Net.Cookie>().FirstOrDefault(x => x.Name == "MyName");
                if (myNameCookie != null)
                {
                    Console.WriteLine("My Name from Cookie is: " + myNameCookie.Value);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}