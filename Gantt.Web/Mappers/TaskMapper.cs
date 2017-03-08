using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gantt.Web.Models.ViewModels;
using Gantt.Web.Models.Entities;

namespace Gantt.Web.Mappers
{
    public class TaskMapper
    {
        internal static Task MapViewModelToItem(TaskViewModel model)
        {
            return new Task
            {
                task_id = model.Id,
                name = model.Name,
                start_date = model.StartDate,
                end_date = model.EndDate,
                created = model.CreateDate,
            };
        }

        internal static List<TaskViewModel> MapItemToViewModel(IEnumerable<Task> tasks)
        {
            return tasks.Select(t => new TaskViewModel
            {
                Id = t.task_id,
                Name = t.name,
                StartDate = t.start_date,
                EndDate = t.end_date,
                CreateDate = t.created
            }).ToList();
        }

        internal static TaskViewModel MapItemToViewModel(Task item)
        {
            return new TaskViewModel
            {
                Id = item.task_id,
                Name = item.name,
                StartDate = item.start_date,
                EndDate = item.end_date,
                CreateDate = item.created
            };
        }
    }
}