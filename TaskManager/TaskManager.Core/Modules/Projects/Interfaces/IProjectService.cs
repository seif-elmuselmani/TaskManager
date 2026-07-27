using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Common;
using TaskManager.Core.Modules.Projects.DTOs;

namespace TaskManager.Core.Modules.Projects.Interfaces
{
    public interface IProjectService
    {
        Task<Result<ProjectResponseDto>> GetProjectByIdAsync(Guid id);
        Task<Result<IEnumerable<ProjectResponseDto>>> GetAllProjectsAsync();
        Task<Result<Guid>> CreateProjectAsync(CreateProjectDto dto);
        Task<Result<bool>> UpdateProjectAsync(UpdateProjectDto dto);
        Task<Result<bool>> DeleteProjectAsync(Guid id);
    }
}
