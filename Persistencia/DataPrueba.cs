using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    NombreCompleto = "Jose Carlos",
                    UserName = "Jose",
                    Email = "jose.carlos@gmail.com",

                };

                await usuarioManager.CreateAsync(usuario, "Password123$");
            }
        }
    }
}