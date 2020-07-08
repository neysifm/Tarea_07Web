using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea_07Web.Models;

namespace Tarea_07Web.Models
{
    public class AppDbContext : IdentityDbContext
    {

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Empleado> Empleados { get; set; }


        public AppDbContext() { }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Tarea_07Web.Models.Ejemplo> Ejemplo { get; set; }
    }
}
