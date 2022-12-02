﻿using SDQRealEstate.Core.Domain.Common;
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
        public String? TipoPropiedad { get; set; }
        public String? TipoVenta { get; set; }
        public String? ImgUrl { get; set; }

        public String? Descripcion { get; set; }
        public double Precio { get; set; }

        public int CantHabitaciones { get; set; }
        public int CantBanos { get; set; }
        public int Metros { get; set; }

        public ICollection<Fotos>? Fotos { get; set; }
    }
}