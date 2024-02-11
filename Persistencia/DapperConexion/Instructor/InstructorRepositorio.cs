using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public class InstructorRepositorio : IInstructor
    {
        private readonly IFactoryConnection _factoryConnection;

        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }
     
        public Task<int> Actualizar(InstructorModel parametros)
        {
            throw new NotImplementedException();
        }

        public Task<int> Eliminar(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<InstructorModel> ObtenerPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<InstructorModel>> ObtenerLista()
        {
            throw new NotImplementedException();
        }
    }
}