using WorkManagementSystem.Interfaces;
using WorkManagementSystem.Entities;
using System.Collections.Generic;
using WorkManagementSystem.Data;
using WorkManagementSystem.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WorkManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly MyAppData _dbcontext;
        private readonly IMapper _mapper;

        public TaskService(MyAppData dbcontext, IMapper mapper)
        {
            this._dbcontext = dbcontext;
            this._mapper = mapper;
        }
        //Return services
        public IEnumerable<TaskDTO> GetTasks()
        {
            var tasksResult = _dbcontext.Task
                .Include(t => t.worker)
                .ToList();
            var tasks = _mapper.Map<List<TaskDTO>>(tasksResult);
            return tasks;
        }

        public TaskDTO GetTask(int id)
        {
            var taskResult = _dbcontext.Task
                .Include(t => t.worker)
                .SingleOrDefault(x => x.id == id);
            var task = _mapper.Map<TaskDTO>(taskResult);
            return task;
        }

        public WorkerDTO GetWorkerTask(int workerID)
        {
            var taskResult = _dbcontext.Task.Where(x => x.worker.id == workerID).ToList();
            var tasks = _mapper.Map<List<WorkerTaskDTO>>(taskResult);
            var workerResult = _dbcontext.Worker.Single(w => w.id == workerID);
            var worker = _mapper.Map<WorkerDTO>(workerResult);
            worker.tasks = tasks;
            return worker;
        }

        public int CreateTask(CreateTaskDTO task)
        {
            var currentTask = _dbcontext.Task.FirstOrDefault(t => t.worker.id == task.workerId && t.endDate == null);
            if (currentTask != null) return -1;
            var newTask = _mapper.Map<TaskEntity>(task);
            newTask.worker = _dbcontext.Worker.Single(x => x.id == task.workerId);
            _dbcontext.Task.Add(newTask);
            _dbcontext.SaveChanges();
            return newTask.id;
        }
        
        public int DeleteTask(int id)
        {
            var task = _dbcontext.Task.FirstOrDefault(x => x.id == id);
            if (task != null)
            {
                _dbcontext.Task.Remove(task);
                _dbcontext.SaveChanges();
                return 1;
            }
            return -1;

        }

        public int UpdateTask(CreateTaskDTO task)
        {
            var newTask = _mapper.Map<TaskEntity>(task);
            newTask.worker = _dbcontext.Worker.Single(x => x.id == task.workerId);
            _dbcontext.Update(newTask);
            _dbcontext.SaveChanges();
            return 1;
        }
    }
}
