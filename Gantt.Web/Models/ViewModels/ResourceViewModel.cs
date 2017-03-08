using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gantt.Web.Models.ViewModels
{
    public class ResourceViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Resource's first name is required")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Resource's first name is required")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }

}