using System;
using System.ComponentModel.DataAnnotations;


namespace WorkManagementSystem.Entities
{
    public class WorkerEntity
    {
        [Display(Name = "Worker ID")]
        public int id { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "Length of {0} must be maximum {1} charcters")]
        [Required(ErrorMessage = "{0} is required.")]
        public string name { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Length of {0} must be maximum {1} charcters")]
        [Required(ErrorMessage = "{0} is required.")]
        public string lastName { get; set; } = string.Empty;

        [Display(Name = "login")]
        [Required(ErrorMessage = "{0} is required.")]
        public string login { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required")]
        public string password { get; set; } = string.Empty;

        public virtual Role role { get; set; }
    }
}
