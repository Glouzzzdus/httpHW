using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

class Client
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        string[] urls = new[]
        {
            "http://localhost:8888/Information/",
            "http://localhost:8888/Success/",
            "http://localhost:8888/Redirection/",
            "http://localhost:8888/ClientError/",
            "http://localhost:8888/ServerError/",
            "http://localhost:8888/MyNameByHeader/"
        };

        foreach (var url in urls)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(url + " " + (int)response.StatusCode + " " + response.StatusCode); // Print url and status code

                if (response.Headers.Contains("X-MyName")) // If header "X-MyName" is available
                {
                    var myName = response.Headers.GetValues("X-MyName").First();
                    Console.WriteLine("My Name is: " + myName); // Print the value of header
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