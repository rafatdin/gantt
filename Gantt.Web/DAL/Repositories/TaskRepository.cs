using Gantt.Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gantt.Web.DAL.Repositories
{
    public class TaskRepository : IRepository<Task>, IDisposable
    {
        private readonly GanttContext context;

        public TaskRepository(GanttContext context)
        {
            this.context = context;
        }

        #region InterfaceMethods
        public void Insert(Task item)
        {
            context.Tasks.Add(item);
        }

        public void Delete(int itemId)
        {
            Task student = context.Tasks.Find(itemId);
            context.Tasks.Remove(student);
        }

        public void Update(Task student)
        {
            context.Entry(student).State = EntityState.Modified;
        }

        public IEnumerable<Task> SearchFor(System.Linq.Expressions.Expression<Func<Task, bool>> predicate)
        {
            return context.Tasks.Where(predicate).AsEnumerable();
        }

        public IEnumerable<Task> GetAll()
        {
            return context.Tasks.ToList();
        }

        public Task GetById(int id)
        {
            return context.Tasks.Find(id);
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