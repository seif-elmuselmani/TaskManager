using System;

namespace TaskManager.Core.Modules.Projects.DTOs
{
    public class UpdateProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
