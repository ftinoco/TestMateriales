using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestMaterialesAPP.Tools;
using TestMaterialesAPP.ViewModels;

namespace TestMaterialesAPP.Controllers
{
    public class CategoriaController : Controller
    {
        private HttpClientService _httpClientService;

        public CategoriaController(IHttpClientService httpClientService)
        {
            _httpClientService = (HttpClientService)httpClientService;
            _httpClientService.EndPointUrl = "Categoria";
        }

        [HttpGet("Categoria")]
        public async Task<ActionResult> IndexAsync()
        {
            var response = await _httpClientService.GetAsync<CategoriaViewModel>();
            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
