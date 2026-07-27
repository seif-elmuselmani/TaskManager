using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManager.Core.Modules.Tasks.DTOs;
using TaskManager.Core.Modules.Tasks.Interfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _taskService.GetTaskByIdAsync(id);
            if (!result.IsSuccess) return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(Guid projectId)
        {
            var result = await _taskService.GetTasksForProjectAsync(projectId);
            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(Core.Modules.Tasks.Enums.TaskStatus status)
        {
            var result = await _taskService.FilterTasksByStatusAsync(status);
            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            var result = await _taskService.CreateTaskAsync(dto);
            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");

            var result = await _taskService.UpdateTaskAsync(dto);
            if (!result.IsSuccess) return NotFound(result.ErrorMessage);

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] Core.Modules.Tasks.Enums.TaskStatus newStatus)
        {
            var result = await _taskService.UpdateTaskStatusAsync(id, newStatus);
            if (!result.IsSuccess) return NotFound(result.ErrorMessage);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result.IsSuccess) return NotFound(result.ErrorMessage);

            return NoContent();
        }
    }
}
