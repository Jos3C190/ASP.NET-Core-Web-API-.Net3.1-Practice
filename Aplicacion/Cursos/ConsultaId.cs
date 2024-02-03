using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Dominio;
using Persistencia;
using System.Net;
using Aplicacion.ManejadorError;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDto> {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, CursoDto> 
        {
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;
            public Manejador(CursosOnlineContext context, IMapper mapper) 
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso
                .Include(x => x.InstructoresLink)
                .ThenInclude(y => y.Instructor)
                .FirstOrDefaultAsync(a => a.CursoId == request.Id);

                if (curso == null) {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso" });
                }

                var cursoDto = _mapper.Map<Curso, CursoDto>(curso);
                return cursoDto;
            }
        }
    }
}