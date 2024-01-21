using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Dominio;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<Curso> {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, Curso> 
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context) 
            {
                _context = context;
            }

            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.Id);
                return curso;
            }
        }
    }
}