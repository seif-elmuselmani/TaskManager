using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Modules.Tasks.Entities;

namespace TaskManager.Core.Modules.Tasks.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(Enums.TaskStatus status);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);
    }
}
