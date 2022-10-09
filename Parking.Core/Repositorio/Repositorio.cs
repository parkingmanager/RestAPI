using Parking.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Repositorio
{
    public class ParkingRepositorio : IDisposable
    {
        /// <summary>
        /// Parametros
        /// </summary>
        
        ParkingEntities dc;
        public ParkingEntities DataContext
        {
            get
            {
                if (this.dc == null)
                {
                    this.dc = new ParkingEntities();
                }

                return this.dc;
            }
            set
            {
                if (this.dc != null)
                    this.dc.Dispose();

                this.dc = value;
            }
        }

        public bool LazyLoading
        {
            get
            {
                return this.DataContext.Configuration.LazyLoadingEnabled;
            }
            set
            {
                this.DataContext.Configuration.LazyLoadingEnabled = value;
            }
        }

        public virtual IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = this.DataContext.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public virtual IQueryable<T> GetAll<T>(string includeProperties = "") where T : class
        {
            IQueryable<T> query = this.DataContext.Set<T>().AsQueryable();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return query;
        }

        public virtual T GetById<T>(object id) where T : class
        {
            return this.DataContext.Set<T>().Find(id);
        }

        public virtual void Insert<T>(T entity) where T : class
        {  
            PropertyInfo FechaCreacion = entity.GetType().GetProperty("FechaCreacion");
            if (FechaCreacion !=null)
            {
                FechaCreacion.SetValue(entity, DateTime.Now,null);
            }
            this.DataContext.Set<T>().Add(entity);
        }

        public virtual void Delete<T>(object id) where T : class
        {
            T entityToDelete = this.DataContext.Set<T>().Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete<T>(T entityToDelete) where T : class
        {
            if (this.DataContext.Entry(entityToDelete).State == EntityState.Detached)
                
                this.DataContext.Set<T>().Attach(entityToDelete);

            //this.DataContext.Entry(entityToDelete).State = EntityState.Modified;
            this.DataContext.Set<T>().Remove(entityToDelete);
            //this.DataContext.Set<T>().Find(entityToDelete);
            //this.DataContext.
        }

        public virtual void Update<T>(T entityToUpdate) where T : class
        {
            this.DataContext.Set<T>().Attach(entityToUpdate);
            this.DataContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public int Commit()
        {
            try
            {
                return this.DataContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw ex;
            }            
        }

        public void Dispose()
        {
            if (this.dc != null)
                this.dc.Dispose();

            this.dc = null;
        }
    }
}
