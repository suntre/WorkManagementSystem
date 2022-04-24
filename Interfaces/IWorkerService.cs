using System.Collections.Generic;
using WorkManagementSystem.Entities;
using WorkManagementSystem.Models;

namespace WorkManagementSystem.Interfaces
{
    public interface IWorkerService
    {
        IEnumerable<WorkerListDTO> GetWorkers();
        WorkerListDTO GetWorkerById(int id);
        IEnumerable<WorkerListDTO> GetByRole(string roleName);
        int CreateWorker(CreateWorkerDTO worker);
        int UpdateWorker(WorkerEntity worker);
        int DeleteWorker(int id);
        string Login(UserLoginDTO userToLogin);

        string CreateToken(WorkerEntity user);

    }
}
