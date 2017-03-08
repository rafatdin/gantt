using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gantt.Web.Models.ViewModels;
using Gantt.Web.Models.Entities;
using System.Globalization;

namespace Gantt.Web.Mappers
{
    public class AppointMapper
    {
        internal static ResourceInTask MapViewModelToItem(ResourceInTaskVM model)
        {
            return new ResourceInTask
            {
                id = model.Id,
                resource_id = model.Resource.Id,
                task_id = model.TaskId,
                date_from = model.DateFrom,
                date_to = model.DateTo,
                created = model.Created
            };
        }
        internal static ResourceInTask MapViewModelToItem(AppointDTO model)
        {
            return new ResourceInTask
            {
                resource_id = int.Parse(model.resource_id),
                task_id = int.Parse(model.task_id),
                date_from = DateTime.ParseExact(model.start_date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                date_to = DateTime.ParseExact(model.end_date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                created = DateTime.Today
            };
        }

        internal static ResourceInTaskVM MapItemToViewModel(ResourceInTask item)
        {
            return new ResourceInTaskVM
            {
                Id = item.id,
                Resource = ResourceMapper.MapItemToViewModel(item.Resource),
                TaskId = item.task_id,
                DateFrom = item.date_from,
                DateTo = item.date_to,
                Created = item.created
            };
        }

        public static List<ResourceInTaskVM> MapItemsToViewModel(IEnumerable<ResourceInTask> items)
        {
            return items.Select(x => new ResourceInTaskVM
            {
                Id = x.id,
                Resource = ResourceMapper.MapItemToViewModel(x.Resource),
                TaskId = x.task_id,
                DateFrom = x.date_from,
                DateTo = x.date_to,
                Created = x.created
            }).ToList();
        }

        public static List<AppointViewModel> MapAllItemsToViewModel(IEnumerable<ResourceInTask> items, IEnumerable<Resource> allResources)
        {
            var result = items.Select(x => new AppointViewModel
            {
                Task = TaskMapper.MapItemToViewModel(x.Task),
                Resources = MapItemsToViewModel(items.Where(i => i.resource_id == x.resource_id && i.task_id == x.task_id))
            }).ToList();

            foreach (var item in result)
                item.AllResources = ResourceMapper.MapItemToViewModel(allResources.Where(r => !item.Resources.Any(vm => vm.Resource.Id == r.resource_id)));

            return result;
        }

        internal static IEnumerable<ResourceInTask> MapViewModelToItems(AppointViewModel model)
        {
            return model.Resources.Select(x => new ResourceInTask
            {
                id = x.Id,
                resource_id = x.Resource.Id,
                task_id = x.TaskId,
                date_from = x.DateFrom,
                date_to = x.DateTo,
                created = x.Created
            });
        }
    }
}