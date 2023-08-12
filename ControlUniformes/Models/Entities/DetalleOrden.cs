using System;
using System.Collections.Generic;

namespace ControlUniformes.Models.Entities
{
    public partial class DetalleOrden
    {
        public int IdDetalle { get; set; }
        public int IdOrden { get; set; }
        public string Articulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int TotalPiezas { get; set; }
        public string Tallas { get; set; } = null!;
        public int PrecioUnitario { get; set; }
        public int ImporteTotal { get; set; }
        public virtual OrdenesProduccion? IdOrdenNavigation { get; set; } = null!;
    }
}
