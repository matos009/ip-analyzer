using Newtonsoft.Json;

public class IpData
{
    [JsonProperty("ip")]
    public string Ip { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }
}