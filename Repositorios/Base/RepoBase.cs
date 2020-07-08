using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tarea_07Web.Models;
using System.Linq.Expressions;

namespace Tarea_07Web.Repositorios.Base
{
    public abstract class RepoBase<TEntity> : IRepoBase<TEntity> where TEntity : class, ICamposControl
    {
        private readonly AppDbContext _repoContext;
        public RepoBase(AppDbContext repoContext)
        {
            _repoContext = repoContext;
        }
        public IQueryable<TEntity> BuscarPorCondicion(Expression<Func<TEntity, bool>> expression)
        {
            return _repoContext.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public async Task<TEntity> BuscarPorId(int? id)
        {
            return await _repoContext.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> BuscarTodo()
        {
            return _repoContext.Set<TEntity>().AsNoTracking();
        }

        public async Task Crear(TEntity entity)
        {
            try
            {
                entity.Creado = DateTime.Now;
                entity.Modificado = DateTime.Now;
                entity.Inactivo = false;
                await _repoContext.Set<TEntity>().AddAsync(entity);
                await _repoContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task Eliminar(TEntity entity)
        {
            try
            {
                _repoContext.Set<TEntity>().Update(entity);
                entity.Modificado = DateTime.Now;
                _repoContext.Entry(entity).Property(c => c.Creado).IsModified = false;
                _repoContext.Entry(entity).Property(c => c.Inactivo).IsModified = false;
                entity.Inactivo = true;
                await _repoContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Modificar(TEntity entity)
        {
            try
            {
                _repoContext.Set<TEntity>().Update(entity);
                entity.Modificado = DateTime.Now;
                _repoContext.Entry(entity).Property(c => c.Creado).IsModified = false;
                _repoContext.Entry(entity).Property(c => c.Inactivo).IsModified = false;
                await _repoContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

