using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Tarea_07Web.Models;
using Tarea_07Web.Repositorios.Base;

namespace Tarea_07Web.Controllers
{
    [BreadCrumb(Title = "Empleados", Url = "/Empleados/Index", Order = 0)]
    public class EmpleadoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepoWrapper _repo;

        public EmpleadoController(AppDbContext context, IRepoWrapper repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: EmpleadoController
        [BreadCrumb(Title = "Listado de Empleados", Order = 1)]
        public async Task<IActionResult> Index(string filter, int page = 1,
                                         string sortExpression = "Nombre")
        {
            var filtrada = _repo.Empleados.BuscarPorCondicion(x => x.Inactivo == false).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filtrada = filtrada.Where(p => p.Nombre.Contains(filter));
            }

            var model = await PagingList.CreateAsync(filtrada, 2, page, sortExpression, "Nombre");

            model.RouteValue = new RouteValueDictionary {

                  {"filter", filter }
            };

            model.Action = "Index";
            return View(model);
        }

        // GET: EmpleadoController/Details/5
        [BreadCrumb(Title = "Detalle de Empleado", Order = 2)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleados = await _repo.Empleados.BuscarPorId(id);
            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        // GET: EmpleadoController/Create
        [BreadCrumb(Title = "Crear Empleado", Order = 3)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmpleadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Cedula,FechaNacimiento,FechaIngreso,Nombre,Apellido,Sexo,EstadoCivil,Ocupacion,TipoSangre,Nacionalidad,Religion,Email,Direccion,Salariomensual,Departamento,Contactoemergencia,AFP,ARS,Observaciones")] Empleado empleados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleados);
        }

        // GET: EmpleadoController/Edit/5
        [BreadCrumb(Title = "Editar Empleado", Order = 4)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleados = await _repo.Empleados.BuscarPorId(id);
            if (empleados == null)
            {
                return NotFound();
            }
            return View(empleados);
        }

        // POST: EmpleadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Cedula,FechaNacimiento,FechaIngreso,Nombre,Apellido,Sexo,EstadoCivil,Ocupacion,TipoSangre,Nacionalidad,Religion,Email,Direccion,Salariomensual,Departamento,Contactoemergencia,AFP,ARS,Observaciones")] Empleado empleados)
        {
            if (id != empleados.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(empleados);
                    //await _context.SaveChangesAsync();
                    await _repo.Empleados.Modificar(empleados);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadosExists(empleados.Id))
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
            return View(empleados);
        }

        private bool EmpleadosExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }

        // GET: EmpleadoController/Delete/5
        [BreadCrumb(Title = "Eliminar Empleado", Order = 1)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleados = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        // POST: EmpleadoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleados = await _context.Empleados.FindAsync(id);
            //_context.Empleados.Remove(empleados);
            //await _context.SaveChangesAsync();
            await _repo.Empleados.Eliminar(empleados);
            return RedirectToAction(nameof(Index));
        }
    }
}
