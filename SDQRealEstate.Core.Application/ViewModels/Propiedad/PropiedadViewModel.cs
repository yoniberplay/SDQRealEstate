using SDQRealEstate.Core.Application.ViewModels.Fotos;
using SDQRealEstate.Core.Application.ViewModels.TipoPropiedad;
using SDQRealEstate.Core.Application.ViewModels.Mejoras;
using SDQRealEstate.Core.Application.ViewModels.TipoVenta;
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
        //public String? Mejoras { get; set; }
        public String? ImgUrl { get; set; }
        public int MejorasId { get; set; }

        public String? Descripcion { get; set; }
        public double Precio { get; set; }

        public int CantHabitaciones { get; set; }
        public int CantBanos { get; set; }
        public int Metros { get; set; }

        public ICollection<FotoViewModel>? fotos;
        public TipoPropiedadViewModel? tipoPropiedades { get; set; }
        public TipoVentaViewModel? tipoVenta { get; set; }
        public MejoraViewModel? Mejoras { get; set; }

    }
}
