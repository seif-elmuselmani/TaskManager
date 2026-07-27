using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Common;
using TaskManager.Core.Modules.Tasks.DTOs;
using TaskManager.Core.Modules.Tasks.Enums;

namespace TaskManager.Core.Modules.Tasks.Interfaces
{
    public interface ITaskService
    {
        Task<Result<TaskResponseDto>> GetTaskByIdAsync(Guid id);
        Task<Result<IEnumerable<TaskResponseDto>>> GetTasksForProjectAsync(Guid projectId);
        Task<Result<IEnumerable<TaskResponseDto>>> FilterTasksByStatusAsync(Enums.TaskStatus status);
        Task<Result<Guid>> CreateTaskAsync(CreateTaskDto dto);
        Task<Result<bool>> UpdateTaskAsync(UpdateTaskDto dto);
        Task<Result<bool>> UpdateTaskStatusAsync(Guid id, Enums.TaskStatus newStatus);
        Task<Result<bool>> DeleteTaskAsync(Guid id);
    }
}
