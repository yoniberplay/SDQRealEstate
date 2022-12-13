using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.Agentes.Commands.ChangeStatus
{
    /// <summary>
    /// Parámetros para cambiar el estatus del agente
    /// </summary>  
    public class ChangeStatusCommand : IRequest<int>
    {
        /// <example>PS5</example>
        [SwaggerParameter(Description = "El Id del agente")]
        public string Id { get; set; }
    }

    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IManageUsersService _imanageUsersService;

        public ChangeStatusCommandHandler(IMapper mapper, IManageUsersService imanageUsersService)
        {
            _mapper = mapper;
            _imanageUsersService = imanageUsersService;
        }

        public async Task<int> Handle(ChangeStatusCommand command, CancellationToken cancellationToken)
        {
            var userList = await _imanageUsersService.ChangeStatusUser(command.Id);
            return userList;
        }

        
    }


}
