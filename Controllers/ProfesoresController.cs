using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReflectionIT.Mvc.Paging;
using Tarea_07Web.Models;
using Tarea_07Web.Repositorios.Base;

namespace Tarea_07Web.Controllers
{
    [BreadCrumb(Title = "Profesores", Url = "/Profesores/Index", Order = 0)]
    public class ProfesorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepoWrapper _repo;

        public ProfesorController(AppDbContext context, IRepoWrapper repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: ProfesorController
        [BreadCrumb(Title = "Listado de Profesores", Order = 1)]
        public async Task<IActionResult> Index(string filter, int page = 1,
                                          string sortExpression = "Nombre")
        {
            var filtrada = _repo.Profesores.BuscarPorCondicion(x => x.Inactivo == false).AsNoTracking().AsQueryable();

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

        // GET: ProfesorController/Details/5
        [BreadCrumb(Title = "Detalle de Profesores", Order = 2)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var profesores = await _context.Profesores
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var profesores = await _repo.Profesores.BuscarPorId(id);
            if (profesores == null)
            {
                return NotFound();
            }
            return View(profesores);
        }

        // GET: ProfesorController/Create
        [BreadCrumb(Title = "Crear Profesor", Order = 3)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfesorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Cedula,FechaNacimiento,FechaIngreso,Nombre,Apellido,Sexo,EstadoCivil,Ocupacion,TipoSangre,Nacionalidad,Religion,Email,Direccion,Carrera,Mayorgradoacademico,Categoriaprofesional,Facultad,Asignaturas,Observaciones")] Profesor profesores)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(profesores);
                //await _context.SaveChangesAsync();

                await _repo.Profesores.Crear(profesores);
                return RedirectToAction(nameof(Index));
            }
            return View(profesores);
        }

        // GET: ProfesorController/Edit/5
        [BreadCrumb(Title = "Editar Profesor", Order = 4)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var profesores = await _context.Profesores.FindAsync(id);
            var profesores = await _repo.Profesores.BuscarPorId(id);
            if (profesores == null)
            {
                return NotFound();
            }
            return View(profesores);
        }

        // POST: ProfesorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Cedula,FechaNacimiento,FechaIngreso,Nombre,Apellido,Sexo,EstadoCivil,Ocupacion,TipoSangre,Nacionalidad,Religion,Email,Direccion,Carrera,Mayorgradoacademico,Categoriaprofesional,Facultad,Asignaturas,Observaciones")] Profesor profesores)
        {
            if (id != profesores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(profesores);
                    //await _context.SaveChangesAsync();
                    await _repo.Profesores.Modificar(profesores);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesoresExists(profesores.Id))
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
            return View(profesores);
        }
        private bool ProfesoresExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }

        // GET: ProfesorController/Delete/5
        [BreadCrumb(Title = "Eliminar Profesor", Order = 5)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesores == null)
            {
                return NotFound();
            }
            return View(profesores);
        }

        // POST: ProfesorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesores = await _context.Profesores.FindAsync(id);
            ////_context.Profesores.Remove(profesores);
            ////await _context.SaveChangesAsync();

            await _repo.Profesores.Eliminar(profesores);
            return RedirectToAction(nameof(Index));
        }
    }
}
