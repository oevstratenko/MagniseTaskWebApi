namespace MagniseTaskBE
{
    public interface ICryptoService
    {
        Task<string[]> GetListAsync();
        Task<CryptoItem> GetExchangeAsync(string name);
    }
}
