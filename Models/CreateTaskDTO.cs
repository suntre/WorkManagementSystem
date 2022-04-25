using System.ComponentModel.DataAnnotations;

namespace WorkManagementSystem.Models
{
    public class CreateTaskDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessage ="Title is required")]
        public string title { get; set; }
        public string? description { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int workerId { get; set; }


    }
}
