using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using Persistencia;
using MediatR;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext _context)
            {
                context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Curso = await _context.Curso.FindAsync(request.Id);
                if (Curso == null)
                {
                    throw new Exception("El curso no existe");
                }

                _context.Curso.Remove(Curso);
                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0) {
                    return Unit.Value;
                }

                throw new Exception("No se pudieron guardar los cambios");
            }

        }
    }
}