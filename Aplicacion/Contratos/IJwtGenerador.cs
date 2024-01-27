using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio;

namespace Aplicacion.Contratos
{
    public class IJwtGenerador
    {
        string CrearToken(Usuario usuario);
    }
}