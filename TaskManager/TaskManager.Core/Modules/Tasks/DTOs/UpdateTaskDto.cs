using System;
using TaskManager.Core.Modules.Tasks.Enums;

namespace TaskManager.Core.Modules.Tasks.DTOs
{
    public class UpdateTaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Enums.TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
    }
}
