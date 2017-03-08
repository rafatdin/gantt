using Gantt.Web.Models.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Gantt.Web.DAL
{
    public class GanttContext : DbContext
    {
        //public GanttContext()
        //    : base("name=GanttContext") { }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ResourceInTask> ResourcesInTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>().ToTable("Resources");
            modelBuilder.Entity<Task>().ToTable("Tasks");
            modelBuilder.Entity<ResourceInTask>().ToTable("ResourceInTasks");
        }
        
    }
}