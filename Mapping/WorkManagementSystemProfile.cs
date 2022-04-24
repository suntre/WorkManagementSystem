using AutoMapper;
using WorkManagementSystem.Models;
using WorkManagementSystem.Entities;

namespace WorkManagementSystem.Mapping
{
    public class WorkManagementSystemProfile : Profile
    {
        public WorkManagementSystemProfile()
        {
            CreateMap<TaskEntity, TaskDTO>()
                .ForMember(m => m.workerLastName, c => c.MapFrom(s => s.worker.lastName))
                .ForMember(m => m.workerName, c => c.MapFrom(s => s.worker.name))
                .ForMember(m => m.workerId, c => c.MapFrom(s => s.worker.id));
            CreateMap<WorkerEntity, WorkerDTO>()
                .ForMember(m => m.roleName, c => c.MapFrom(s => s.role.name));
            CreateMap<TaskEntity, WorkerTaskDTO>();
            CreateMap<TaskDTO, TaskEntity>()
                .ForPath(m => m.worker.id, c => c.MapFrom(s => s.workerId))
                .ForPath(m => m.worker.name, c => c.MapFrom(s => s.workerName))
                .ForPath(m => m.worker.lastName, c => c.MapFrom(s => s.workerLastName));
            CreateMap<CreateTaskDTO, TaskEntity>();
            CreateMap<CreateWorkerDTO, WorkerEntity>();
            CreateMap<WorkerEntity, WorkerListDTO>()
                .ForMember(m => m.roleName, c => c.MapFrom(s => s.role.name));
        }
    }
}
