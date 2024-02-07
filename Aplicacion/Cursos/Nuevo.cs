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
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public List<Guid> ListaInstructor { get; set; }
            public decimal Precio { get; set; }
            public decimal Promocion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
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
                var _cursoId = Guid.NewGuid();
                var curso = new Curso
                {
                    CursoId = _cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };
                _context.Curso.Add(curso);

                if(request.ListaInstructor != null)
                {
                    foreach (var id in request.ListaInstructor)
                    {
                        var cursoInstructor = new CursoInstructor
                        {
                            CursoId = curso.CursoId,
                            InstructorId = id
                        };
                        _context.CursoInstructor.Add(cursoInstructor);
                    }
                }

                var precioEntidad = new Precio
                {
                    PrecioId = Guid.NewGuid(),
                    PrecioActual = request.Precio,
                    Promocion = request.Promocion,
                    CursoId = _cursoId
                };

                _context.Precio.Add(precioEntidad);

                var valor = await _context.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(System.Net.HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el curso" });
            }
        }
    }
}