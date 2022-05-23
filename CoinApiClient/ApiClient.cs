using CoinApiClient.Entity;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace CoinApiClient
{
    public static class ApiClient
    {
        public static readonly ApiOptions options = new ApiOptions();

        #region Http

        static readonly HttpClient client = new HttpClient();

        public static async Task<AssetItem[]> GetAssetsAsync(string asset_ids)
        {
            var sb = new StringBuilder(options.httpClient.Url)
                .Append("assets");
            if (asset_ids != null && asset_ids.Length > 0)
            {
                sb.Append($"?filter_asset_id={asset_ids}");
            }

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(sb.ToString()),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("X-CoinAPI-Key", options.Key);

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            
            var items = JsonSerializer.Deserialize<AssetItem[]>(responseBody);

            return items;
        }

        #endregion

        #region WS

        public static async Task<T> WsGetDataAsync<T>(WsSubscriptionItem Item)
        {
            using (var ws = new ClientWebSocket())
            {
                await ws.ConnectAsync(new Uri(options.wsClient.Url), CancellationToken.None);

                var msg = JsonSerializer.Serialize(Item);

                ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));

                await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);

                var buffer = new byte[1024 * 4];

                var receiveResult = await ws.ReceiveAsync(buffer, CancellationToken.None);
                var wsData = Encoding.UTF8.GetString(buffer.TakeWhile(x => x != 0).ToArray());

                return JsonSerializer.Deserialize<T>(wsData);
            }
        }

        #endregion
    }
}