using SDQRealEstate.Core.Application.Dtos.Propiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Dtos.Fotos
{
    public class FotosResponse
    {
        public  int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public int PropiedadId { get; set; }
        public string? ImageUrl { get; set; }

        public String UserId { get; set; }

        public PropiedadResponse? propiedad { get; set; }

    }
}
