using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.Propiedad
{
    public class SavePropiedadViewModel 
    {
        public int Id { get; set; }
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Debe colocar el Tipo de Propiedad")]
        [DataType(DataType.Text)]
        public int? TipoPropiedadId { get; set; }

        [Required(ErrorMessage = "Debe colocar el Tipo de Venta")]
        [DataType(DataType.Text)]
        public int? TipoVentaId { get; set; }

        [Required(ErrorMessage = "Debe colocar la mejora")]
        [DataType(DataType.Text)]
        public int? MejorasId { get; set; }

        public String? ImgUrl { get; set; }


        [Required(ErrorMessage = "Debe colocar el precio de la propiedad")]
        [DataType(DataType.Text)]
        public double? Precio { get; set; }

        [Required(ErrorMessage = "Debe colocar la Descripcion")]
        [DataType(DataType.Text)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Introduzca cantidad de habitaciones")]
        [DataType(DataType.Text)]
        public int CantHabitaciones { get; set; }

        [Required(ErrorMessage = "Introduzca cantidad de baños")]
        [DataType(DataType.Text)]
        public int CantBanos { get; set; }

        [Required(ErrorMessage = "Introduzca cantidad de Metros")]
        [DataType(DataType.Text)]
        public int Metros { get; set; }

        
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File2 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File3 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File4 { get; set; }

       
        public String? UserId { get; set; }


    }
}
