using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestMaterialesAPP.Tools;
using TestMaterialesAPP.ViewModels;

namespace TestMaterialesAPP.Controllers
{ 
    public class MaterialController : Controller
    {

        private HttpClientService _httpClientService;

        public MaterialController(IHttpClientService httpClientService)
        {
            _httpClientService = (HttpClientService)httpClientService;
            _httpClientService.EndPointUrl = "Material";
        }

        [HttpGet("Material")] 
        public async Task<ActionResult> IndexAsync()
        {
            var response = await _httpClientService.GetAsync<MaterialViewModel>();
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("Material/{id}")]
        public async Task<ActionResult> IndexAsync(int id)
        {
            var response = await _httpClientService.GetAllById<MaterialViewModel>(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("Material")]
        public async Task<IActionResult> Post([FromBody] MaterialCrearViewModel model)
        {
            var result = await _httpClientService.PostAsync<MaterialCrearViewModel, int>(model);

            return Ok(result);
        }


        [HttpPut("Material/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MaterialCrearViewModel model)
        {
            var result = await _httpClientService.UpdateAsync(id, model);

            return Ok(result);
        }

        [HttpDelete("Material/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _httpClientService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
