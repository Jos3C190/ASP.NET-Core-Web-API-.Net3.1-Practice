using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Dominio;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public int CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime FechaPublicacion { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public class Manejador()
            {
                private readonly CursosOnlineContext _context;
                public Manejador(CursosOnlineContext context)
                {
                    _context = context;
                }

                public Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
                {
                    var curso = new Curso {
                        Titulo = request.Titulo,
                        Descripcion = request.Descripcion,
                        FechaPublicacion = request.FechaPublicacion
                    };
                    _context.Curso.Add(curso);
                    var valor = await _context.SaveChangesAsync();

                    if (valor>0) {
                        return Unit.Value;
                    }

                    throw new Exception("No se pudo insertar el curso");
                }
            }
        }
    }
}