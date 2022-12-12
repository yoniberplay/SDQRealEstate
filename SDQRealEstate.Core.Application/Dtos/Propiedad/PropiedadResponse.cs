using SDQRealEstate.Core.Application.Dtos.Fotos;
using SDQRealEstate.Core.Application.Dtos.TipoPropiedades;
using SDQRealEstate.Core.Application.Dtos.TipoVenta;
using SDQRealEstate.Core.Application.Dtos.Mejora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Dtos.Propiedad
{
    public class PropiedadResponse
    {
        public int Codigo { get; set; }
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        public String? UserId { get; set; }
        public int TipoPropiedadId { get; set; }
        public int TipoVentaId { get; set; }
        public int MejorasId { get; set; }
        public String? ImgUrl { get; set; }

        public String? Descripcion { get; set; }
        public double Precio { get; set; }

        public int CantHabitaciones { get; set; }
        public int CantBanos { get; set; }
        public int Metros { get; set; }

        public ICollection<FotosResponse>? fotos { get; set; }
        public TipoPropiedadesReponse? tipoPropiedades { get; set; }
        public TipoVentaResponse? tipoVenta { get; set; }
        public MejoraResponse? Mejoras { get; set; }


    }
}
