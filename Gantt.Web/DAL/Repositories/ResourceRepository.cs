using Gantt.Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gantt.Web.DAL.Repositories
{
    public class ResourceRepository : IRepository<Resource>, IDisposable
    {
        private readonly GanttContext context;

        public ResourceRepository(GanttContext context)
        {
            this.context = context;
        }

        #region InterfaceMethods
        public void Insert(Resource item)
        {
            context.Resources.Add(item);
        }

        public void Delete(int itemId)
        {
            Resource student = context.Resources.Find(itemId);
            context.Resources.Remove(student);
        }

        public void Update(Resource student)
        {
            context.Entry(student).State = EntityState.Modified;
        }

        public IEnumerable<Resource> SearchFor(System.Linq.Expressions.Expression<Func<Resource, bool>> predicate)
        {
            return context.Resources.Where(predicate).AsEnumerable();
        }

        public IEnumerable<Resource> GetAll()
        {
            return context.Resources.ToList();
        }

        public Resource GetById(int id)
        {
            return context.Resources.Find(id);
        }


        public void Save()
        {
            context.SaveChanges();
        }

        #endregion

        #region Disposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}