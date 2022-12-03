using SDQRealEstate.Core.Application.ViewModels.TipoVenta;
using SDQRealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Interfaces.Services
{
    public interface ITipoVentaService : IGenericService<SaveTipoVentaViewModel, TipoVentaViewModel,TipoVenta>
    {
        
    }
}
