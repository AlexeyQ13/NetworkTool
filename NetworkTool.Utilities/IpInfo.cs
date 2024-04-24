

using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetworkTool.Utilities;

public class IpInfo
{
    [JsonPropertyName("query")]
    public string? Query { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("countryCode")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("regionName")]
    public string? RegionName { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("zip")]
    public string? Zip { get; set; }

    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lon")]
    public double Lon { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("isp")]
    public string? Isp { get; set; }

    [JsonPropertyName("org")]
    public string? Org { get; set; }

    [JsonPropertyName("as")]
    public string? As { get; set; }

    public static async Task<IpInfo?> GetIpInfoAsync(string ip = "")
    {
        var client = new HttpClient();
        var result = await client.GetStringAsync($"http://ip-api.com/json/{ip}");

        return JsonSerializer.Deserialize<IpInfo>(result);
    }

    public override string ToString() => $"IP: {Query}\n" +
                                         $"Страна: {Country}\n" +
                                         $"Регион: {RegionName}\n" +
                                         $"Город: {City}\n" +
                                         $"Индекс: {Zip}\n" +
                                         $"Широта: {Lat}\n" +
                                         $"Долгота: {Lon}\n" +
                                         $"Часовой пояс: {Timezone}\n" +
                                         $"ISP: {Isp}";
}