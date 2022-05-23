using MagniseTaskBE;
using Microsoft.AspNetCore.Mvc;

namespace MagniseTaskWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoService _service;

        public CryptoController(ICryptoService service)
        {
            _service = service;
        }

        /// <summary> Get crypto currency exchange rate </summary>
        [HttpGet(Name = "GetCrypto")]
        public async Task<CryptoItem> Get(string cryptoName)
        {
            var res = await _service.GetExchangeAsync(cryptoName);

            return res;
        }
    }
}
