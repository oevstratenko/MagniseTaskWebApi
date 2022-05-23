namespace CoinApiClient
{
    public class ApiOptions
    {
        public const string OptionName = "CoinApi";

        public string Key { get; set; }
        public HttpClient httpClient { get; set; }
        public WsClient wsClient { get; set; }

        public class HttpClient
        {
            public string Url { get; set; }
        }

        public class WsClient
        {
            public string Url { get; set; }
        }
    }
}
