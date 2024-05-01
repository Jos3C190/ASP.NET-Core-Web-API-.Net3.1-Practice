using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Dapper;

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

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> InstructorList = null;
            var storeProcedure = "usp_Obtener_Instructores";

            try
            {
                var connection = _factoryConnection.GetConnection();
                InstructorList = (await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo encontrar los instructores", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return InstructorList;
        }

        public async Task<int> Nuevo(string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "usp_Insertar_Instructor";
            try
            {
                var resultado = await connection = _factoryConnection.GetConnection();
                connection.ExecuteAsync(
                    storeProcedure, new
                    {
                        InstructorId = Guid.NewGuid(),
                        Nombre = nombre,
                        Apellidos = apellidos,
                        Titulo = titulo,
                    },
                    commandType: CommandType.StoredProcedure
                    );

                _factoryConnection.CloseConnection();

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo insertar el nuevo instructor", ex);

                return Task.FromResult(0);
            }
        }
    }
}
