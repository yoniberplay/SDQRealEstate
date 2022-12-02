using SDQRealEstate.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Domain.Entities
{
    public class Fotos : AuditableBaseEntity
    {
        public String PropiedadId { get; set; }
        public string? ImageUrl { get; set; }

        public String UserId { get; set; }

        public Propiedad? propiedad { get; set; }
    }
}
