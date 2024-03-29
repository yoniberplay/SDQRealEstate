﻿using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using SDQRealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Interfaces.Services
{
    public interface IPropiedadService : IGenericService<SavePropiedadViewModel, PropiedadViewModel,Propiedad>
    {
        Task<int> GetCantidadTipoPropiedad(int id);
        Task<int> GetCantidadTipoVenta(int id);
        Task<int> GetCantidadMejora(int id);


        Task<List<PropiedadViewModel>> GetAllPropiedadByAgentId(String AgentId);
        Task<List<PropiedadViewModel>> GetAllViewModelIcnlude();

        Task<PropiedadViewModel> GetByCode(int code);
    }
}
