using SDQRealEstate.Core.Application.ViewModels.TipoPropiedad;
using SDQRealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Interfaces.Services
{
    public interface ITipoPropiedadService : IGenericService<SaveTipoPropiedadViewModel, TipoPropiedadViewModel,TipoPropiedades>
    {
        
    }
}
