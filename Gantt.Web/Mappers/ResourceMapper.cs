using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gantt.Web.Models.ViewModels;
using Gantt.Web.Models.Entities;

namespace Gantt.Web.Mappers
{
    public class ResourceMapper
    {
        internal static Resource MapViewModelToItem(ResourceViewModel model)
        {
            return new Resource
            {
                resource_id = model.Id,
                first_name = model.FirstName,
                last_name = model.LastName,
                created = model.CreateDate,
            };
        }

        internal static List<ResourceViewModel> MapItemToViewModel(IEnumerable<Resource> resources)
        {
            return resources.Select(t => new ResourceViewModel
            {
                Id = t.resource_id,
                FirstName = t.first_name,
                LastName = t.last_name,
                CreateDate = t.created
            }).ToList();
        }

        internal static ResourceViewModel MapItemToViewModel(Resource item)
        {
            return new ResourceViewModel
            {
                Id = item.resource_id,
                FirstName = item.first_name,
                LastName = item.last_name,
                CreateDate = item.created
            };
        }
    }
}