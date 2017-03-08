using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gantt.Web.Models.Entities
{
    public class ResourceInTask
    {
        [Key]
        public int id { get; set; }
        public int resource_id { get; set; }
        public int task_id { get; set; }
        public DateTime date_from { get; set; }
        public DateTime date_to { get; set; }
        public DateTime created { get; set; }
        public DateTime? updated { get; set; }

        public virtual Resource Resource { get; set; }
        public virtual Task Task { get; set; }
    }
}