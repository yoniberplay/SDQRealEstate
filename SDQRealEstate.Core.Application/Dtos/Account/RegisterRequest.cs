using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Dtos.Account
{
    public class RegisterRequest
    {
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String UserName { get; set; }
        public String ConfirmPassword { get; set; }
        public String Password { get; set; }
        public String Phone { get; set; }
        public string Foto { get; set; }
        public IFormFile? File { get; set; }
        public string Tipo { get; set; }

    }
}
