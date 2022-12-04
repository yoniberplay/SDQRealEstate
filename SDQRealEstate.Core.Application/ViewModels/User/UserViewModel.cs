using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.User
{
    public class UserViewModel
    {   
        
        public string FirstName { get; set; }

        public  bool EmailConfirmed { get; set; }
        public string LastName { get; set; }
       
        public string Username { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }       

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Tipo { get; set; }

        public string? Foto { get; set; }

        public int CantPropiedades { get; set; }


    }
}
