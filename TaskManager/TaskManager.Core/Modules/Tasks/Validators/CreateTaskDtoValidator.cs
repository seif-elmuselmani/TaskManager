using System;
using FluentValidation;
using TaskManager.Core.Modules.Tasks.DTOs;

namespace TaskManager.Core.Modules.Tasks.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("DueDate must be in the future.");
        }
    }
}
