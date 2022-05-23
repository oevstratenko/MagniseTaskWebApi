using System.Text.Json.Serialization;

public class ExchangeRateItem
{
    public string asset_id_base { get; set; }
    public string asset_id_quote { get; set; }
    public double? rate { get; set; }
    [JsonPropertyName("time")]
    public DateTime? updated { get; set; }
}