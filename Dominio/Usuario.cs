using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Dominio
{
    public class Usuario : IdentityUser
    {
        public string NombreCompleto { get; set; }
    }
}