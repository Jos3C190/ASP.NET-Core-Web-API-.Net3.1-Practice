using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Dominio;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<Curso>> {}

        public class Manejador: IRequestHandler<ListaCursos, List<Curso>>{
            public Task<List<Curso>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}