using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(40) };

    static async Task Main()
    {
        string[] urls = new[]
        {
            "http://localhost:8888/Information/",
            "http://localhost:8888/Success/",
            "http://localhost:8888/Redirection/",
            "http://localhost:8888/ClientError/",
            "http://localhost:8888/ServerError/"
        };

        foreach (var url in urls)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(url + " " + (int)response.StatusCode + " " + response.StatusCode);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception Caught!");
                Console.WriteLine($"Failed at fetching {url}. Message :{e.Message}");
            }
        }
    }
}