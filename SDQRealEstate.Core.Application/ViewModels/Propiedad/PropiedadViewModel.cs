using SDQRealEstate.Core.Application.ViewModels.Fotos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.Propiedad
{
    public class PropiedadViewModel
    {
        public int Id { get; set; }
        public String? UserId { get; set; }
        public String? TipoPropiedad { get; set; }
        public String? TipoVenta { get; set; }
        public String? ImgUrl { get; set; }

        public String? Descripcion { get; set; }
        public double Precio { get; set; }

        public int CantHabitaciones { get; set; }
        public int CantBanos { get; set; }
        public int Metros { get; set; }

        public List<FotoViewModel>? fotos = new();

    }
}
