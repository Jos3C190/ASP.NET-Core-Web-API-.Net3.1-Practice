using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Data;

namespace Persistencia.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;
        private readonly IOptions<ConexionConfiguracion> _configs;
        public FactoryConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }   
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_configs.Value.ConexionSQL);
            }
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }
    }
}