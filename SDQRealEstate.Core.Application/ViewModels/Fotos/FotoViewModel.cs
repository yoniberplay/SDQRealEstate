using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.ViewModels.Fotos
{
    public class FotoViewModel
    {
        public String PropiedadId { get; set; }
        public string? ImageUrl { get; set; }

        public String UserId { get; set; }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
    }
}
