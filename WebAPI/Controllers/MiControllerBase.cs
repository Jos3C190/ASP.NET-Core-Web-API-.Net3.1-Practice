using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers
{
    // Para no inyectar el mediador en cada controlador, se crea esta clase base
    // que hereda de ControllerBase y se inyecta el mediador en esta clase

    [Route("api/[controller]")]
    public class MiControllerBase : ControllerBase
    {
        private IMediator _mediator;
        
        protected IMediator Mediator => _mediator ?? (_mediator = (IMediator) HttpContext.RequestServices.GetService(typeof(IMediator)));

        

    }
}