using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.Fotos
{
    public class SaveFotoViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public int PropiedadId { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string? UserId { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }


    }
}
