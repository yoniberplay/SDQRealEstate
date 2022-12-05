using SDQRealEstate.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Domain.Entities
{
    public class Propiedad : AuditableBaseEntity
    {

        public String? UserId { get; set; }
        public int TipoPropiedadId { get; set; }
        public int TipoVentaId { get; set; }
        public String? ImgUrl { get; set; }

        public String? Descripcion { get; set; }
        public double Precio { get; set; }

        public int CantHabitaciones { get; set; }
        public int CantBanos { get; set; }
        public int Metros { get; set; }

        public ICollection<Fotos>? fotos { get; set; }
        public TipoPropiedades? tipoPropiedades { get; set; }
        public TipoVenta? tipoVenta { get; set; }

    }
}
