using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Dominio;
using Aplicacion.Cursos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CursosController(IMediator mediator) {
            _mediator = mediator;
        }

        // http://localhost:5000/api/Cursos
        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get() {
            return await _mediator.Send(new Consulta.ListaCursos());
        }

        // http://localhost:5000/api/Cursos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(int id) {
            return await _mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data) {
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(int id, Editar.Ejecuta data) {
            data.CursoId = id;
            return await _mediator.Send(data);
        }
    }
}