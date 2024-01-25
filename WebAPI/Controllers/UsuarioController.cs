using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UsuarioController : MiControllerBase
    {

        // http://localhost:5000/api/Usuario/login
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody]Login.Ejecuta data)
        {
            return await Mediator.Send(data);
        }

    }
}