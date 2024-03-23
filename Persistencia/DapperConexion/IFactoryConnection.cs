using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace Persistencia.DapperConexion
{
    public interface IFactoryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection();
    }
}