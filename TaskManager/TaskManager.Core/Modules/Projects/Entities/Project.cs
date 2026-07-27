using System;
using System.Collections.Generic;
using TaskManager.Core.Modules.Tasks.Entities;

namespace TaskManager.Core.Modules.Projects.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
