using System.Collections.Generic;
using WorkManagementSystem.Entities;
using WorkManagementSystem.Models;
namespace WorkManagementSystem.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskDTO> GetTasks();
        public WorkerDTO GetWorkerTask(int workerID);
        TaskDTO GetTask(int id);
        int CreateTask(CreateTaskDTO task);
        int DeleteTask(int id);
        int UpdateTask(CreateTaskDTO task);

    }
}
