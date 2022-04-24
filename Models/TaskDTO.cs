using WorkManagementSystem.Entities;

namespace WorkManagementSystem.Models
{
    public class TaskDTO
    {
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string workerName { get; set; }
        public string workerLastName { get; set; }
        public int workerId { get; set; }
    }
}
