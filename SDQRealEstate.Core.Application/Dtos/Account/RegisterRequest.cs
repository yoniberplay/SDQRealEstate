using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Dtos.Account
{
    /// <summary>
    /// Parámetros para la creacion de usuario basico
    /// </summary> 
    public class RegisterRequest
    {
        [SwaggerParameter(Description = "El correo de el usuario")]
        public String Email { get; set; }
        [SwaggerParameter(Description = "El nombre de el usuario")]
        public String FirstName { get; set; }
        [SwaggerParameter(Description = "El apellido de el usuario")]
        public String LastName { get; set; }
        [SwaggerParameter(Description = "El nombre de usuario")]
        public String UserName { get; set; }
        [SwaggerParameter(Description = "La contrasenia del usuario")]
        public String ConfirmPassword { get; set; }
        [SwaggerParameter(Description = "La contrasenia del usuario")]
        public String Password { get; set; }
        [SwaggerParameter(Description = "El telefono del usuario")]
        public String Phone { get; set; }
        [SwaggerParameter(Description = "La foto del usuario")]
        public string Foto { get; set; }
        public IFormFile? File { get; set; }
        [SwaggerParameter(Description = "El rol del usuario")]
        public string Tipo { get; set; }

    }
}
