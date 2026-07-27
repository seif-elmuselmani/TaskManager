using System;

namespace TaskManager.Core.Modules.Tasks.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskManager.Core.Modules.Tasks.Enums.TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        
        public Guid ProjectId { get; set; }
    }
}
