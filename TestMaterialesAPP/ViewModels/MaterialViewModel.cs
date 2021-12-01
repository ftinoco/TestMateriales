using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaterialesAPP.ViewModels
{
    public class MaterialViewModel
    {
        public int IdMeterial { get; set; }

        public string NombreMaterial { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public string Categoria { get; set; }

        public int IdCategoria { get; set; }

        public string Proveedor { get; set; }

        public string UnidadMedida { get; set; }

        public int IdUnidadMedida { get; set; }

        public decimal Existencia { get; set; }
    }
}
