using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class CursoInstructorDto
    {
        public Guid CursoId { get; set; }
        public Guid InstructorId { get; set; }
    }
}