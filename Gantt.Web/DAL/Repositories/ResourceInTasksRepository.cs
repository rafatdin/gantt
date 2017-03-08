using Gantt.Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gantt.Web.DAL.Repositories
{
    public class ResourceInTaskRepository : IRepository<ResourceInTask>, IDisposable
    {
        private readonly GanttContext context;

        public ResourceInTaskRepository(GanttContext context)
        {
            this.context = context;
        }

        #region InterfaceMethods
        public void Insert(ResourceInTask item)
        {
            context.ResourcesInTasks.Add(item);
        }

        public void Delete(int itemId)
        {
            ResourceInTask student = context.ResourcesInTasks.Find(itemId);
            context.ResourcesInTasks.Remove(student);
        }

        public void Update(ResourceInTask student)
        {
            context.Entry(student).State = EntityState.Modified;
        }

        public IEnumerable<ResourceInTask> SearchFor(System.Linq.Expressions.Expression<Func<ResourceInTask, bool>> predicate)
        {
            return context.ResourcesInTasks.Where(predicate).AsEnumerable();
        }

        public IEnumerable<ResourceInTask> GetAll()
        {
            return context.ResourcesInTasks.ToList();
        }

        public ResourceInTask GetById(int id)
        {
            return context.ResourcesInTasks.Find(id);
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