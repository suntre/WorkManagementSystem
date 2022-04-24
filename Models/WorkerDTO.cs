using WorkManagementSystem.Entities;
using System.Collections.Generic;
namespace WorkManagementSystem.Models
{
    public class WorkerDTO
    {
        public string name { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public List<WorkerTaskDTO>? tasks { get; set; }
        public string roleName { get; set; } 
    }
}
