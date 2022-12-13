using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.User
{
    public class FilterByNameViewModel
    {

        [Required(ErrorMessage = "Debe insertar un nombre para la busqueda")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
