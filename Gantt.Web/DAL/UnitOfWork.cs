using Gantt.Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gantt.Web.DAL
{
    public class UnitOfWork : IDisposable
    {
        private GanttContext context = new GanttContext();
        private GenericRepository<Resource> resourcesRepository;
        private GenericRepository<Task> tasksRepository;
        private GenericRepository<ResourceInTask> res_in_tasks_Repository;


        /*
         * Идет проверка на существование репозитория 
         * если не создан еще, создается новый используя инстанс контекста бд. 
         * в результате, все репозитории шейрят один и тот же инстанс контекста.
         * Тем самым, избегаются так называемые паршл апдейты (напр.: в одной транзакции
         * изменяются две сущности. Если каждый использует отдельные инстансы контекста,
         * часть транзакций с использованием первого контекста может успешно пройти, 
         * а другая часть, использующий второй контекст - не пройти).
         */
        public GenericRepository<Resource> ResourcesRepository
        {
            get
            {

                if (this.resourcesRepository == null)
                {
                    this.resourcesRepository = new GenericRepository<Resource>(context);
                }
                return resourcesRepository;
            }
        }
        public GenericRepository<Task> TasksRepository
        {
            get
            {

                if (this.tasksRepository == null)
                {
                    this.tasksRepository = new GenericRepository<Task>(context);
                }
                return tasksRepository;
            }
        }
        public GenericRepository<ResourceInTask> ResInTasksRepository
        {
            get
            {

                if (this.res_in_tasks_Repository == null)
                {
                    this.res_in_tasks_Repository = new GenericRepository<ResourceInTask>(context);
                }
                return res_in_tasks_Repository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

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
    }
}