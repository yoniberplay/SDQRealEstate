using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Foto { get; set; }

    }
}
