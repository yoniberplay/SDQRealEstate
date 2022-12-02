using SDQRealEstate.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.Admin
{
    public class SaveAdminViewModel : SaveUserViewModel
    {
        [Required(ErrorMessage = "Debe colocar su cedula")]
        [DataType(DataType.Text)]
        public string Cedula { get; set; }
    }
}
