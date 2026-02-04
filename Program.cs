using Newtonsoft.Json;
using System.Net.Http;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        var ips = new List<string>
        {
            "5.18.233.41",
            "46.147.120.9",
            "91.203.56.188",
            "89.163.214.77",
            "138.201.95.12",
            "54.182.91.203",
            "51.158.102.66",
            "133.242.187.19",
            "3.120.54.98",
            "177.72.34.90",
            "203.12.89.45"
        };

        var results = new List<IpData>();

        foreach (var ip in ips)
        {
            var data = await GetIpData(ip);
            if (data != null)
                results.Add(data);
        }

        var countryStats = results
            .GroupBy(x => x.Country)
            .Select(g => new { Country = g.Key, Count = g.Count() })
            .ToList();

        Console.WriteLine("IP count by country:");
        foreach (var c in countryStats)
            Console.WriteLine($"{c.Country} - {c.Count}");

        var topCountry = countryStats
            .OrderByDescending(x => x.Count)
            .First().Country;

        Console.WriteLine($"\nCities in {topCountry}:");

        foreach (var city in results
            .Where(x => x.Country == topCountry)
            .Select(x => x.City)
            .Distinct())
        {
            Console.WriteLine(city);
        }
    }

    static async Task<IpData?> GetIpData(string ip)
    {
        try
        {
            var response = await client.GetStringAsync($"https://ipinfo.io/{ip}/json");
            return JsonConvert.DeserializeObject<IpData>(response);
        }
        catch
        {
            return null;
        }
    }
}