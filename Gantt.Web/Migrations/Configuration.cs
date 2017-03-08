namespace Gantt.Web.Migrations
{
    using Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Gantt.Web.DAL.GanttContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /* 
         * Проект создавался код форстом, для миграции (в вашем случае заполнения базы)
         * 1. Подправьте коннекшн стринг "GanttContext" на созданную вами базу
         * 2. выполните Update-Database -TargetMigration $InitialDatabase
         * 3. Удалите инишл криэйт файл
         * 4. выполните Add-Migration InitialCreate
         * 5. update-database -force
         */
        protected override void Seed(Gantt.Web.DAL.GanttContext context)
        {
            var resources = new List<Resource> {
                  new Resource {first_name = "John", last_name = "Smith", created = new DateTime(2015, 1, 2) },
                  new Resource {first_name = "Nash", last_name = "Freeman", created = new DateTime(2015, 1, 2) },
                  new Resource {first_name = "Rebekah", last_name = "Valencia", created = new DateTime(2015, 1, 2)},
                  new Resource {first_name = "Kyle", last_name = "Porter", created = new DateTime(2015, 1, 2) },
                  new Resource {first_name = "Raymond", last_name = "Ball", created = new DateTime(2015, 1, 2) },
                  new Resource {first_name = "Calista", last_name = "Vance", created = new DateTime(2015, 1, 2) },
                  new Resource {first_name = "Hu", last_name = "Reyes", created = new DateTime(2015, 1, 2) },
                  new Resource {first_name = "Harlan", last_name = "Wooten", created = new DateTime(2015, 1, 2) },
                  new Resource {first_name = "Thaddeus", last_name = "Mack", created = new DateTime(2015, 1, 2) },
                  new Resource {first_name = "Scott", last_name = "Knapp", created = new DateTime(2015, 1, 2) }
            };
            resources.ForEach(x => context.Resources.AddOrUpdate(r => r.first_name, x)); //Assuming that first name of resources are unique (only for Code first)

            var tasks = new List<Task>
            {
                new Task{name =  "Task 1: sed sem egestas", start_date = new DateTime(2016, 7, 13), end_date = new DateTime(2017, 12, 7), created = new DateTime(2016, 7, 19)},
                    new Task{name =  "Task 2: ut mi. Duis risus", start_date = new DateTime(2016, 12, 11), end_date = new DateTime(2017, 10, 18), created = new DateTime(2016, 7, 11)},
                    new Task{name =  "Task 3: urna.", start_date = new DateTime(2016, 12, 31), end_date = new DateTime(2017, 11, 16), created = new DateTime(2016, 3, 28)},
                    new Task{name =  "Task 4: vitae, orci. Phasellus dapibus", start_date = new DateTime(2016, 9, 27), end_date = new DateTime(2017, 9, 27), created = new DateTime(2017, 7, 16)},
                    new Task{name =  "Task 5: mollis", start_date = new DateTime(2016, 11, 9), end_date = new DateTime(2017, 4, 16), created = new DateTime(2017, 5, 22)},
                    new Task{name =  "Task 6: nascetur", start_date = new DateTime(2017, 3, 17), end_date = new DateTime(2017, 12, 19), created = new DateTime(2016, 7, 28)},
                    new Task{name =  "Task 7: erat,", start_date = new DateTime(2016, 11, 11), end_date = new DateTime(2017, 10, 22), created = new DateTime(2017, 9, 22)},
                    new Task{name =  "Task 8: tristique neque venenatis lacus", start_date = new DateTime(2016, 7, 19), end_date = new DateTime(2017, 5, 6), created = new DateTime(2017, 10, 22)},
                    new Task{name =  "Task 9: imperdiet ornare. In faucibus.", start_date = new DateTime(2017, 7, 20), end_date = new DateTime(2017, 9, 30), created = new DateTime(2018, 2, 19)},
                    new Task{name =  "Task 10: fermentum arcu.", start_date = new DateTime(2016, 4, 12), end_date = new DateTime(2017, 3, 11), created = new DateTime(2017, 10, 5)}
            };
            tasks.ForEach(x => context.Tasks.AddOrUpdate(t => t.name, x)); //Assuming that name of tasks are unique (only for Code first)
            context.SaveChanges();

            var resourceInTasks = new List<ResourceInTask>
            {
                new ResourceInTask {
                    resource_id = 1,
                    task_id = tasks.Single(t => t.name == "Task 1: sed sem egestas").task_id,
                    date_from = new DateTime(2016, 7, 13),
                    date_to = new DateTime(2016, 12, 7),
                    created = new DateTime(2016, 7, 13)
                },
                new ResourceInTask {
                    resource_id = resources.Single(r => r.first_name == "Rebekah").resource_id,
                    task_id = tasks.Single(t => t.name == "Task 5: mollis").task_id,
                    date_from = new DateTime(2016, 11, 09),
                    date_to = new DateTime(2017, 1, 19),
                    created = new DateTime(2016, 11, 09)
                },
                new ResourceInTask {
                    resource_id = resources.Single(r => r.first_name == "Thaddeus").resource_id,
                    task_id = tasks.Single(t => t.name == "Task 5: mollis").task_id,
                    date_from = new DateTime(2017, 1, 20),
                    date_to = new DateTime(2017, 4, 16),
                    created = new DateTime(2016, 11, 09)
                }
            };

            foreach (ResourceInTask item in resourceInTasks)
            {
                var ritInDb = context.ResourcesInTasks.Where(
                    rit =>
                        rit.Resource.resource_id == item.resource_id &&
                        rit.Task.task_id == item.task_id).SingleOrDefault();

                if (ritInDb == null) //check if this item was already exists, if not add to DB
                {
                    context.ResourcesInTasks.Add(item);
                }
            }
            context.SaveChanges();
        }
    }
}
