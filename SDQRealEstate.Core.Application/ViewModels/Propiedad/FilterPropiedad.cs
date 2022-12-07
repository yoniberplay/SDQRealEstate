using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.Propiedad
{
    public class FilterPropiedad
    {
        public int Codigo { get; set; }
        public int TipoPropiedadId { get; set; }
        public int PrecioMin { get; set; }
        public int PrecioMax { get; set; }
        public int CantHabitaciones { get; set; }
        public int CantBanos { get; set; }
    }
}
