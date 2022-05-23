namespace CoinApiClient.Entity
{
    public class WsSubscriptionItem
    {
        public string type { get; set; } = "hello";
        public string apikey { get; set; }

        public bool heartbeat { get; set; }
        public string[] subscribe_data_type { get; set; }
        public string[] subscribe_filter_asset_id { get; set; }
    }
}
