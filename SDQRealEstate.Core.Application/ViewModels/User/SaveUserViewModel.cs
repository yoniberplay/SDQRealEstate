﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {   
        [Required(ErrorMessage = "Debe colocar el nombre")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        //[Required(ErrorMessage = "Debe colocar una contraseña")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
         ErrorMessage = "Contraseña poco segura, debe colocar entre 8 y 15 caracteres incluyendo Mayusculas, Minusculas y @#$%^&*()_+")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }       

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar un telefono")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Seleccione Tipo de cuenta")]
        [DataType(DataType.Text)]
        public string Tipo { get; set; }

        public string? Foto { get; set; }

        //[Required(ErrorMessage = "Tiene que usar una foto para mostrar a los demas")]
        //[DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        public bool HasError { get; set; } 
        public string? Error { get; set; }
        public string? Id { get; set; }
    }
}
