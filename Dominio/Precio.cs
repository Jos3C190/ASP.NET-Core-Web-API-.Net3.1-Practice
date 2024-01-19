using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Precio
    {
       public int PrecioId { get; set; } 
       public decimal PrecioActual { get; set; }
       public decimal Promocion { get; set; }
       public int CursoId { get; set; }
       
    }
}