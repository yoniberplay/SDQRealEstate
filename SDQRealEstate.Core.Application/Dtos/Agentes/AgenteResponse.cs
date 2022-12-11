using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Dtos.Agentes
{
    public class AgenteResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CantPropiedades { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
