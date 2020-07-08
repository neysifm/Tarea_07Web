using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea_07Web.Models;

namespace Tarea_07Web.Repositorios.Base
{
    public class EmpleadosRepo : RepoBase<Empleado>, IEmpleadosRepo
    {
        private readonly AppDbContext _context;

        public EmpleadosRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
