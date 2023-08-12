using System;
using System.Collections.Generic;

namespace ControlUniformes.Models.Entities
{
    public partial class OrdenesProduccion
    {
        public OrdenesProduccion()
        {
            DetalleOrdens = new HashSet<DetalleOrden>();
        }

        public int IdOrden { get; set; }
        public string Pedido { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Maquilero { get; set; } = null!;
        public string OrdenProduccion { get; set; } = null!;
        public int? Total { get; set; }

        public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; }
    }
}
