using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestMaterialesAPP.Tools;
using TestMaterialesAPP.ViewModels;

namespace TestMaterialesAPP.Controllers
{
    public class UnidadMedidaController : Controller
    {
        private HttpClientService _httpClientService;

        public UnidadMedidaController(IHttpClientService httpClientService)
        {
            _httpClientService = (HttpClientService)httpClientService;
            _httpClientService.EndPointUrl = "UnidadMedida";
        }

        [HttpGet("UnidadMedida")]
        public async Task<ActionResult> IndexAsync()
        {
            var response = await _httpClientService.GetAsync<UnidadMedidaViewModel>();
            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
