using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Common;
using TaskManager.Core.Modules.Projects.Interfaces;
using TaskManager.Core.Modules.Tasks.DTOs;
using TaskManager.Core.Modules.Tasks.Entities;
using TaskManager.Core.Modules.Tasks.Enums;
using TaskManager.Core.Modules.Tasks.Interfaces;

namespace TaskManager.Core.Modules.Tasks.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        private TaskResponseDto MapToDto(TaskItem task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId
            };
        }

        public async Task<Result<TaskResponseDto>> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return Result<TaskResponseDto>.Failure("Task not found.");

            return Result<TaskResponseDto>.Success(MapToDto(task));
        }

        public async Task<Result<IEnumerable<TaskResponseDto>>> GetTasksForProjectAsync(Guid projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null) return Result<IEnumerable<TaskResponseDto>>.Failure("Project not found.");

            var tasks = await _taskRepository.GetTasksByProjectIdAsync(projectId);
            return Result<IEnumerable<TaskResponseDto>>.Success(tasks.Select(MapToDto));
        }

        public async Task<Result<IEnumerable<TaskResponseDto>>> FilterTasksByStatusAsync(Enums.TaskStatus status)
        {
            var tasks = await _taskRepository.GetTasksByStatusAsync(status);
            return Result<IEnumerable<TaskResponseDto>>.Success(tasks.Select(MapToDto));
        }

        public async Task<Result<Guid>> CreateTaskAsync(CreateTaskDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);
            if (project == null) return Result<Guid>.Failure("Project not found.");

            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                ProjectId = dto.ProjectId,
                Status = Enums.TaskStatus.ToDo
            };

            await _taskRepository.AddAsync(task);
            return Result<Guid>.Success(task.Id);
        }

        public async Task<Result<bool>> UpdateTaskAsync(UpdateTaskDto dto)
        {
            var existingTask = await _taskRepository.GetByIdAsync(dto.Id);
            if (existingTask == null) return Result<bool>.Failure("Task not found.");

            if (existingTask.ProjectId != dto.ProjectId)
            {
                var newProject = await _projectRepository.GetByIdAsync(dto.ProjectId);
                if (newProject == null) return Result<bool>.Failure("New Project not found.");
            }

            existingTask.Title = dto.Title;
            existingTask.Description = dto.Description;
            existingTask.DueDate = dto.DueDate;
            existingTask.ProjectId = dto.ProjectId;
            existingTask.Status = dto.Status;

            await _taskRepository.UpdateAsync(existingTask);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateTaskStatusAsync(Guid id, Enums.TaskStatus newStatus)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return Result<bool>.Failure("Task not found.");

            task.Status = newStatus;
            await _taskRepository.UpdateAsync(task);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteTaskAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return Result<bool>.Failure("Task not found.");

            await _taskRepository.DeleteAsync(task);
            return Result<bool>.Success(true);
        }
    }
}
