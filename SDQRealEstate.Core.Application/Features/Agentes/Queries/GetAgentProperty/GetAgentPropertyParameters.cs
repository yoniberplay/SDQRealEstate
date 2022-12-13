using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.Agentes.Queries.GetAgentProperty
{
    public class GetAgentPropertyParameters
    {
        [SwaggerParameter(Description = "Debe colocar el id del agente")]
        [Required]
        public String Id { get; set; }
    }
}
