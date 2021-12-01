using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMaterialesAPI.Model;

namespace TestMaterialesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly TestContext context;
        public MaterialController(TestContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lst = context.Material.Join(context.UnidadMedida,
                        x => x.IdUnidadMedida,
                        y => y.IdUnidadMedida,
                        (mat, um) => new { mat, um }).Join(context.Categoria,
                        r => r.mat.IdCategoria,
                        c => c.IdCategoria,
                        (mat_um, cat) => new { mat_um, cat }
                        ).Select(x => new
                        {
                            x.mat_um.mat.IdMeterial,
                            x.mat_um.mat.NombreMaterial,
                            x.mat_um.mat.Descripcion,
                            x.mat_um.mat.Precio,
                            x.cat.IdCategoria,
                            Categoria = x.cat.Descripcion,
                            x.mat_um.mat.Proveedor,
                            x.mat_um.um.IdUnidadMedida,
                            UnidadMedida = x.mat_um.um.Descripcion,
                            x.mat_um.mat.Existencia
                        }).ToList();
            if (lst == null && lst.Count == 0)
                return StatusCode(404, new
                {
                    message = "No se encontraron registros"
                });

            return StatusCode(200, lst);
        }

        [HttpGet("{id}", Name = "GetByID")]
        public IActionResult GetByID(int id)
        {
            var material = context.Material.Find(id);
            if (material == null)
                return StatusCode(404, new
                {
                    message = $"No se encontró registro con el identificador: {id}"
                });

            return StatusCode(200, material);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Material data)
        {
            try
            {
                if (data != null)
                {
                    if (string.IsNullOrWhiteSpace(data.NombreMaterial))
                        return StatusCode(400, new { message = "Nombre requerido" });

                    if (string.IsNullOrWhiteSpace(data.Descripcion))
                        return StatusCode(400, new { message = "Descripción requerido" });

                    if (string.IsNullOrWhiteSpace(data.Proveedor))
                        return StatusCode(400, new { message = "Proveedor requerido" });

                    if (data.IdCategoria == 0)
                        return StatusCode(400, new { message = "Categoría requerido" });

                    if (data.IdUnidadMedida == 0)
                        return StatusCode(400, new { message = "Unidad de medida requerido" });

                    context.Material.Add(data);
                    context.SaveChanges();

                    return StatusCode(200, new { message = "Registro guardado exitosamente" });
                }
                return StatusCode(400, new
                {
                    message = "Se debe proporcionar la información a registrar"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error inesperado",
                    exception = ex
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Material data)
        {
            try
            {
                if (data != null)
                {
                    if (string.IsNullOrWhiteSpace(data.NombreMaterial))
                        return StatusCode(400, new { message = "Nombre requerido" });

                    if (string.IsNullOrWhiteSpace(data.Descripcion))
                        return StatusCode(400, new { message = "Descripción requerido" });

                    if (string.IsNullOrWhiteSpace(data.Proveedor))
                        return StatusCode(400, new { message = "Proveedor requerido" });

                    if (data.IdCategoria == 0)
                        return StatusCode(400, new { message = "Categoría requerido" });

                    if (data.IdUnidadMedida == 0)
                        return StatusCode(400, new { message = "Unidad de medida requerido" });

                    var material = context.Material.Find(id);
                    if (material == null)
                        return StatusCode(400, new { message = $"No se encontró registro con el identificador: {id}" });

                    material.Descripcion = data.Descripcion;
                    material.NombreMaterial = data.NombreMaterial;
                    material.Proveedor = data.Proveedor;
                    material.IdCategoria = data.IdCategoria;
                    material.IdUnidadMedida = data.IdUnidadMedida;
                    material.Precio = data.Precio;
                    material.Existencia = data.Existencia;

                    context.SaveChanges();

                    return StatusCode(200, new { message = "Registro actualizado exitosamente" });
                }
                return StatusCode(400, new
                {
                    message = "Se debe proporcionar la información a registrar"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error inesperado",
                    exception = ex
                });
            }
        }

        /// <summary>
        /// Deactivate record by id
        /// </summary>
        /// <param name="id">Id of record</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var material = context.Material.Find(id);
                if (material == null)
                    return StatusCode(400, new { message = $"No se encontró registro con el identificador: {id}" });

                context.Material.Remove(material);
                context.SaveChanges();

                return StatusCode(200, new { message = "Registro eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error inesperado",
                    exception = ex
                });
            }
        }


    }
}
