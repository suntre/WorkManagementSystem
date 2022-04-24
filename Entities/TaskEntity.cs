using System;
using System.ComponentModel.DataAnnotations;

namespace WorkManagementSystem.Entities
{
    public class TaskEntity
    {
        public int id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} is required.")]
        public string title { get; set; } = string.Empty;

        [Display(Name = "Task Description")]
        public string? description { get; set; }

        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }

        [Display(Name = "Finish Date")]
        public DateTime? endDate { get; set; }

        [Display(Name = "Task Performer")]
        public virtual WorkerEntity worker { get; set; }
    }
}
