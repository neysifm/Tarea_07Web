using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DNTBreadCrumb.Core;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;
using Tarea_07Web.Models;
using Tarea_07Web.Repositorios.Base;

namespace Assignment_7.Controllers
{
    [BreadCrumb(Title = "Estudiantes", Url = "/Estudiantes/Index", Order = 0)]
    public class EstudiantesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepoWrapper _repo;
        public EstudiantesController(AppDbContext context, IRepoWrapper repo)
        {
            _context = context;
            _repo = repo;

        }

        // GET: Estudiantes
        [BreadCrumb(Title = "Listado de Estudiantes", Order = 1)]
        public async Task<IActionResult> Index(string filter, int page = 1,
                                          string sortExpression = "Nombre")
        {
            var filtrada = _repo.Estudiantes.BuscarPorCondicion(x => x.Inactivo == false).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filtrada = filtrada.Where(p => p.Nombre.Contains(filter));
            }

            var model = await PagingList.CreateAsync(filtrada, 1, page, sortExpression, "Nombre");

            model.RouteValue = new RouteValueDictionary {

                  {"filter", filter }
            };

            model.Action = "Index";
            return View(model);
        }

        // GET: Estudiantes/Details/5
        [BreadCrumb(Title = "Detalles de Estudiantes", Order = 2)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var estudiantes = await _context.Estudiantes
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var estudiantes = await _repo.Estudiantes.BuscarPorId(id);
            if (estudiantes == null)
            {
                return NotFound();
            }

            return View(estudiantes);
        }

        // GET: Estudiantes/Create
        [BreadCrumb(Title = "Crear Estudiantes", Order = 3)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Matricula,Cedula,FechaNacimiento,FechaIngreso,Nombre,Apellido,Sexo,EstadoCivil,Ocupacion,TipoSangre,Nacionalidad,Religion,Email,NombrePadre,NombreMadre,Direccion,TipoColegio,Carrera,Observaciones")] Estudiante estudiantes)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(estudiantes);
                //await _context.SaveChangesAsync();

                await _repo.Estudiantes.Crear(estudiantes);
                return RedirectToAction(nameof(Index));
            }
            return View(estudiantes);
        }

        // GET: Estudiantes/Edit/5
        [BreadCrumb(Title = "Editar Estudiantes", Order = 4)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var estudiantes = await _context.Estudiantes.FindAsync(id);
            var estudiantes = await _repo.Estudiantes.BuscarPorId(id);
            if (estudiantes == null)
            {
                return NotFound();
            }
            return View(estudiantes);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricula,Cedula,FechaNacimiento,FechaIngreso,Nombre,Apellido,Sexo,EstadoCivil,Ocupacion,TipoSangre,Nacionalidad,Religion,Email,NombrePadre,NombreMadre,Direccion,TipoColegio,Carrera,Observaciones")] Estudiante estudiantes)
        {
            if (id != estudiantes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(estudiantes);
                    //await _context.SaveChangesAsync();
                    await _repo.Estudiantes.Modificar(estudiantes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudiantesExists(estudiantes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estudiantes);
        }

        // GET: Estudiantes/Delete/5
        [BreadCrumb(Title = "Eliminar Estudiantes", Order = 5)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantes = await _context.Estudiantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiantes == null)
            {
                return NotFound();
            }

            return View(estudiantes);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiantes = await _context.Estudiantes.FindAsync(id);
            //_context.Estudiantes.Remove(estudiantes);
            //await _context.SaveChangesAsync();

            await _repo.Estudiantes.Eliminar(estudiantes);
            return RedirectToAction(nameof(Index));
        }

        private bool EstudiantesExists(int id)
        {
            return _context.Estudiantes.Any(e => e.Id == id);
        }
    }
}
