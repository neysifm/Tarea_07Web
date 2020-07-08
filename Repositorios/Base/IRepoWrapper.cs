using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarea_07Web.Repositorios.Base
{
    public interface IRepoWrapper
    {
        IEstudiantesRepo Estudiantes { get; }
        IEmpleadosRepo Empleados { get; }
        IProfesoresRepo Profesores { get; }
    }
}
