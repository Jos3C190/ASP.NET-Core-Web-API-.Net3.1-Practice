using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Dominio;
using Persistencia;
using FluentValidation;
using System.Net;
using Aplicacion.ManejadorError;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public int CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta> {
            public EjecutaValidacion() {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId);

                if (curso == null) {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso" });
                }

                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion != default ? request.FechaPublicacion : curso.FechaPublicacion;
                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0) {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al actualizar el curso" });

            }
        }
    }
}