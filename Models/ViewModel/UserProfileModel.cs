using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APISeguraCRUD.Models.ViewModel
{
    public class UserProfileModel
    {
        public int UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        //clave foranea de mi tabla UserID
        public int UserID { get; set; }
    }
}