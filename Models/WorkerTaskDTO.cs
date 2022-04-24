namespace WorkManagementSystem.Models
{
    public class WorkerTaskDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
