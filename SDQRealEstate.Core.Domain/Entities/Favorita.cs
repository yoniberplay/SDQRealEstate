using SDQRealEstate.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Domain.Entities
{
    public class Favorita : AuditableBaseEntity
    {

        public String IdUser { get; set; }
        public int IdPropiedad { get; set; }
        public Propiedad? propiedad { get; set; }
    }
}
