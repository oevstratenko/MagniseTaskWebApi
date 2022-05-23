using MagniseTaskBE;
using Microsoft.AspNetCore.Mvc;

namespace MagniseTaskWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoListController : ControllerBase
    {
        private readonly ICryptoService _service;

        public CryptoListController(ICryptoService service)
        {
            _service = service;
        }

        /// <summary> Get array of available crypto currency </summary>
        /// <remarks> Filter of available crypto currency in cofiguration file </remarks>
        [HttpGet(Name = "GetCryptoList")]
        public async Task<string[]> Get()
        {
            return await _service.GetListAsync();
        }
    }
}
