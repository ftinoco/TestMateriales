using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaterialesAPP.ViewModels
{
    public class MaterialCrearViewModel
    {
        public string NombreMaterial { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int IdCategoria { get; set; }

        public string Proveedor { get; set; }

        public int IdUnidadMedida { get; set; }

        public decimal Existencia { get; set; }
    }
}
