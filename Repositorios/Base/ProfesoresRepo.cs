using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea_07Web.Models;

namespace Tarea_07Web.Repositorios.Base
{
    public class ProfesoresRepo : RepoBase<Profesor>, IProfesoresRepo
    {
        private AppDbContext _context;

        public ProfesoresRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
