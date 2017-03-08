using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gantt.Web.Models.Entities
{
    public class Resource
    {
        [Key]
        public int resource_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime created { get; set; }
        public DateTime? updated { get; set; }

        public virtual ICollection<ResourceInTask> ResourcesInTasks { get; set; }
    }
}