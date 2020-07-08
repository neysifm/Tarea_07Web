using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea_07Web.Models;

namespace Tarea_07Web.Repositorios.Base
{
    public class RepoWrapper : IRepoWrapper
    {
        private readonly AppDbContext _repoContext;
        private IEstudiantesRepo _estudiantes;
        private IEmpleadosRepo _empleados;
        private IProfesoresRepo _profesores;
        public RepoWrapper(AppDbContext repoContext)
        {
            _repoContext = repoContext;
        }
        public IEstudiantesRepo Estudiantes
        {
            get
            {
                if (_estudiantes == null)
                {
                    _estudiantes = new EstudiantesRepo(_repoContext);
                }

                return _estudiantes;
            }
        }

        public IEmpleadosRepo Empleados
        {
            get
            {
                if (_empleados == null)
                {
                    _empleados = new EmpleadosRepo(_repoContext);
                }

                return _empleados;
            }
        }


        public IProfesoresRepo Profesores
        {
            get
            {
                if (_profesores == null)
                {
                    _profesores = new ProfesoresRepo(_repoContext);
                }

                return _profesores;
            }
        }
    }
}
