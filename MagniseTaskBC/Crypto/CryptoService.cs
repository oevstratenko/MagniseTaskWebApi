using CoinApiClient;
using CoinApiClient.Entity;
using MagniseTaskBE;
using MagniseTaskDAC.Entity;

namespace MagniseTaskBC
{
    public class CryptoService : ICryptoService
    {
        public class CryptoSetting
        {
            public const string OptionName = "CryptoSettings";

            public string Filter { get; set; }
        }

        public static readonly CryptoSetting options = new CryptoSetting();

        private readonly IMagniseTaskRepository repo;

        public CryptoService(IMagniseTaskRepository repository)
        {
            repo = repository;
        }

        /// <summary> Get array of available crypto currency </summary>
        public async Task<string[]> GetListAsync()
        {
            var assets = await ApiClient.GetAssetsAsync(options.Filter);

            var cryptoList = assets.Select(x => new CryptoItem
            {
                Id          = x.asset_id,
                Name        = x.name
            }).ToArray();

            var dbItems = repo.NoTrack<Crypto>().ToArray();

            foreach (var crypto in cryptoList)
            {
                //if crypto currency not exists in storage then add it
                if (!dbItems.Any(x => x.Id == crypto.Id))
                {
                    var newDbItem = new Crypto
                    {
                        Id          = crypto.Id,
                        Name        = crypto.Name
                    };

                    repo.AddObject(newDbItem);
                }
            }

            await repo.SaveChangesAsync();

            return cryptoList.Select(x => x.Id).ToArray();
        }

        /// <summary> Process crypto currency exchange rate </summary>
        public async Task<CryptoItem> GetExchangeAsync(string name)
        {            
            var dbItem = repo.Set<Crypto>().FirstOrDefault(x => x.Id == name);
            if (dbItem == null)//use only stored crypto currency
            {
                throw new Exception($"Parameter {name} is not valid");
            }

            //get exchange rate from web socket
            var item = await ApiClient.WsGetDataAsync<ExchangeRateItem>(
                new WsSubscriptionItem
                {
                    apikey                      = ApiClient.options.Key,
                    subscribe_data_type         = new[] { "exrate" },
                    subscribe_filter_asset_id   = new[] { $"{name}/USD" }
                });

            if (dbItem.Price_usd != item.rate)//update price if changed
            {
                if (dbItem.Price_usd != null)
                {
                    //write to history table
                    var newDbCryptoHistory = new CryptoHistory
                    {
                        Crypto      = dbItem,
                        Name        = dbItem.Name,
                        Updated     = dbItem.Updated,
                        Price_usd   = dbItem.Price_usd
                    };
                    repo.AddObject(newDbCryptoHistory);
                }

                dbItem.Updated = item.updated;
                dbItem.Price_usd = item.rate;
            }

            await repo.SaveChangesAsync();

            var res = new CryptoItem
            {
                Id          = dbItem.Id,
                Price_usd   = dbItem.Price_usd,
                Updated     = dbItem.Updated,
                Name        = dbItem.Name,
            };

            return res;
        }
    }
}