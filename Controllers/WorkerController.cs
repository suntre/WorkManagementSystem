using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkManagementSystem.Interfaces;
using WorkManagementSystem.Models;
using WorkManagementSystem.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WorkManagementSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService service)
        {
            _workerService = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WorkerListDTO>> GetWorkers()
        {
            var workers = _workerService.GetWorkers();
            if (workers.Count() == 0) return NotFound("Workers not found");
            return Ok(workers);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<WorkerListDTO> GetWorker(int id)
        {
            if(id < 0) return BadRequest("Id must be higher or equal to one");
            var worker = _workerService.GetWorkerById(id);
            if(worker != null) return Ok(worker);
            return NotFound("User not found");
        }
        [HttpGet]
        [Route("/workers/{roleName}")]
        public ActionResult<IEnumerable<WorkerListDTO>> GetWorkerByRole(string roleName)
        {
            var workers = _workerService.GetByRole(roleName);
            if (workers == null) return BadRequest("Invalid role name");
            if(workers.Count() == 0) return NotFound("No workers found with given role");
            return Ok(workers);
        }
        
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Superior,Manager")]
        public ActionResult CreateWorker(CreateWorkerDTO worker)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = _workerService.CreateWorker(worker);
            if (result == -1) return BadRequest("Given username already exists");
            else if (result == -2) return BadRequest("Given role didn't exists");
            return Created($"/worker/{result}", null);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Manager, Superior")]
        public ActionResult DeleteWorker(int id)
        {
            if (id < 1) return BadRequest("ID  must be higher or equals to one");
            int result = _workerService.DeleteWorker(id);
            if (result == -1) return NotFound("Worker not found");
            return NoContent();
        }
        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "Manager, Superior")]
        public ActionResult UpdateWorker(WorkerEntity worker)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _workerService.UpdateWorker(worker);
            return NoContent();
        }

        [HttpPost]
        [Route("/user/login")]
        [AllowAnonymous]
        public ActionResult<string> LoginUser(UserLoginDTO user)
        {
            if (user == null) return BadRequest("All data need to be fullfilled");
            if (user.password == null) return BadRequest("Password need to be fullfilled");
            if (user.login == null) return BadRequest("Login need to be fullfilled");
            var result = _workerService.Login(user);
            if (result == string.Empty) return BadRequest("Wrong username or password");
            return Ok(result);

        }
    }
}
