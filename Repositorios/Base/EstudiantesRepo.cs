using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea_07Web.Models;

namespace Tarea_07Web.Repositorios.Base
{
    public class EstudiantesRepo : RepoBase<Estudiante>, IEstudiantesRepo
    {

        private readonly AppDbContext _context;

        public EstudiantesRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
