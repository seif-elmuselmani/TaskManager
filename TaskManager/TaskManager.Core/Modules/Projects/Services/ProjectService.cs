using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Common;
using TaskManager.Core.Modules.Projects.DTOs;
using TaskManager.Core.Modules.Projects.Entities;
using TaskManager.Core.Modules.Projects.Interfaces;

namespace TaskManager.Core.Modules.Projects.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Result<ProjectResponseDto>> GetProjectByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return Result<ProjectResponseDto>.Failure("Project not found.");

            var dto = new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
            return Result<ProjectResponseDto>.Success(dto);
        }

        public async Task<Result<IEnumerable<ProjectResponseDto>>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            
            var dtos = projects.Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            });
            
            return Result<IEnumerable<ProjectResponseDto>>.Success(dtos);
        }

        public async Task<Result<Guid>> CreateProjectAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _projectRepository.AddAsync(project);
            return Result<Guid>.Success(project.Id);
        }

        public async Task<Result<bool>> UpdateProjectAsync(UpdateProjectDto dto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(dto.Id);
            if (existingProject == null)
                return Result<bool>.Failure("Project not found.");

            existingProject.Name = dto.Name;
            existingProject.Description = dto.Description;

            await _projectRepository.UpdateAsync(existingProject);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteProjectAsync(Guid id)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
                return Result<bool>.Failure("Project not found.");

            await _projectRepository.DeleteAsync(existingProject);
            return Result<bool>.Success(true);
        }
    }
}
