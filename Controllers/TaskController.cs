using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkManagementSystem.Interfaces;
using System.Collections.Generic;
using WorkManagementSystem.Entities;
using WorkManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace WorkManagementSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController( ITaskService service)
        {
            _taskService = service;
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Superior")]
        public ActionResult<IEnumerable<TaskDTO>> GetListOfTask()
        {
            var result = _taskService.GetTasks();
            if(result.Count() == 0)
            {
                return NotFound("Tasks not found");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TaskDTO> GetTask(int id)
        {
            if (id < 1) return BadRequest("ID must be higher or equal to 1");
            var result = _taskService.GetTask(id);
            if (result == null) return NotFound("Task with given id didn't exists");
            return Ok(result);
        }

        [HttpGet]
        [Route("worker/{workerId}")]
        public ActionResult<WorkerDTO> GetWorkerTask(int workerId)
        {
            var result = _taskService.GetWorkerTask(workerId);
            if (result == null) return NotFound("Worker with given ID not found");
            if (result.tasks == null) return NotFound("Worker haven't got any task");
            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateTask(CreateTaskDTO task)
        {
            if (task.title == null) return BadRequest("Title cannot be empty");
            task.workerId = int.Parse(HttpContext.User.Claims.Where(c => c.Type.Contains("nameidentifier")).First().Value.ToString());
            if (!ModelState.IsValid) return BadRequest(ModelState);
            int id = _taskService.CreateTask(task);
            if (id == -1) return BadRequest("Cannot create task while there is active one");
            return Created($"/task/{id}", null);
        }

        [HttpDelete]
        [Authorize(Roles = "Manager,Superior")]
        [Route("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var result = _taskService.DeleteTask(id);
            if(result == -1) return NotFound("Task with given id didn't exist");
            return NoContent();
        }

        [HttpPost]
        [Route("end")]
        public ActionResult EndTask(CreateTaskDTO task)
        {
            int workerId = int.Parse(HttpContext.User.Claims.Where(c => c.Type.Contains("nameidentifier")).First().Value.ToString());
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if (task.workerId != workerId) return BadRequest("Task can be ended only by his perfomer");
            if(task.endDate == DateTime.MinValue || task.endDate == null)
            {
                task.endDate = DateTime.Now;
                _taskService.UpdateTask(task);
                return NoContent();
            }
            return BadRequest("Cannot end task which has asigned end date");
        }

        [HttpPost]
        [Route("update/{task.id}")]
        public ActionResult UpdateTask(CreateTaskDTO task)
        {
            int workerId = int.Parse(HttpContext.User.Claims.Where(c => c.Type.Contains("nameidentifier")).First().Value.ToString());
            string workerRole = HttpContext.User.Claims.Where(c => c.Type.Contains("role")).First().Value.ToString();

            if(workerId != task.workerId)
            {
                if (workerRole != "Manager" && workerRole != "Superior") return BadRequest("Task can be updated only by his performer or Manager/Superior");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if(task.endDate != null && task.startDate > task.endDate) return BadRequest("Finish date can't be earlier than start date");
            _taskService.UpdateTask(task);
            return NoContent();
        }
    }
}
