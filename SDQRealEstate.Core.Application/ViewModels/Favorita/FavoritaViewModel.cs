using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.Favorita
{
    public class FavoritaViewModel
    {
        public int Id { get; set; }
        public String IdUser { get; set; }
        public int IdPropiedad { get; set; }

        public PropiedadViewModel? propiedad { get; set; }
    }
}
