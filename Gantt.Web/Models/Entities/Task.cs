using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gantt.Web.Models.Entities
{
    public class Task
    {
        [Key]
        public int task_id { get; set; }
        public string name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime created { get; set; }
        public DateTime? updated { get; set; }

        public virtual ICollection<ResourceInTask> ResourcesInTasks { get; set; }
    }
}