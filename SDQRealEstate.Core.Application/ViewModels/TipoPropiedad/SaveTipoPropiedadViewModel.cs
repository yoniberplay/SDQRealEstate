using Microsoft.AspNetCore.Http;
using SDQRealEstate.Core.Application.ViewModels.TipoVenta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.TipoPropiedad
{
    public class SaveTipoPropiedadViewModel  
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public String? Name { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public String? Description { get; set; }


    }
}
