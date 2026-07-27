using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Modules.Projects.Entities;

namespace TaskManager.Core.Modules.Projects.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
    }
}
