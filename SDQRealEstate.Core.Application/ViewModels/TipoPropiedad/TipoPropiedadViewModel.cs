using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.TipoPropiedad
{
    public class TipoPropiedadViewModel 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int Cantidad { get; set; }
    }
}
