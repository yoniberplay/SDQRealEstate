using Microsoft.AspNetCore.Http;
using SDQRealEstate.Core.Application.ViewModels.TipoVenta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.Favorita
{
    public class SaveFavoritaViewModel  
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Debe introducir el id del usuario")]
        [DataType(DataType.Text)]
        public String? IdUser { get; set; }

        [Required(ErrorMessage = "Debe introducir el id de la propiedad")]
        [DataType(DataType.Text)]
        public int IdPropiedad { get; set; }



    }
}
