﻿using SDQRealEstate.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Domain.Entities
{
    public class Mejora : AuditableBaseEntity
    {    
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Propiedad>? propiedad { get; set; }

    }
}
