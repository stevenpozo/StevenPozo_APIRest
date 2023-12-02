using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APISeguraCRUD.Models.ViewModel
{
    public class LoginModel
    {
        public int? UserId { get; set; }
        [DisplayName("Usuario")]
        public string Username { get; set; }
        [DisplayName("Contraseña")]
        public string Password { get; set; }
    }
}