using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestMaterialesAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace TestMaterialesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly TestContext context;
        
        public UnidadMedidaController(TestContext ctx)
        {
            context = ctx;
        }

        [HttpGet] 
        public IActionResult Get()
        {
            var lst = context.UnidadMedida.ToList();
            if (lst == null && lst.Count == 0)
                return StatusCode(404, new
                {
                    message = "No se encontraron registros"
                });

            return StatusCode(200, lst);
        } 
    }
}
