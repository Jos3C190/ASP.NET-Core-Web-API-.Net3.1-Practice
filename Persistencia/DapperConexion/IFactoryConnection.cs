using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion
{
    public interface IFactoryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection();
    }
}