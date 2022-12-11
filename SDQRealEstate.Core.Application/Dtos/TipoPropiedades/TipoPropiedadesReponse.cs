﻿using SDQRealEstate.Core.Application.Dtos.Propiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Dtos.TipoPropiedades
{
    public class TipoPropiedadesReponse
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }


    }
}
