using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gantt.Web.Models.ViewModels
{
    public class AppointViewModel
    {        
        public List<ResourceInTaskVM> Resources { get; set; }
        public TaskViewModel Task { get; set; }

        public List<ResourceViewModel> AllResources { get; set; }
    }

    public class ResourceInTaskVM
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int TaskId { get; set; }

        public ResourceViewModel Resource { get; set; }

        [Required(ErrorMessage = "Date from which the resource starts task is required")]
        [DataType(DataType.Date)]
        [Display(Name = "From date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }

        [Required(ErrorMessage = "Date when the resource should finish the task is required")]
        [DataType(DataType.Date)]
        [Display(Name = "To date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
    }

    public class AppointDTO
    {
        public string status { get; set; }
        public string task_id { get; set; }
        public string resource_id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }
}