using System;
using FluentValidation;
using TaskManager.Core.Modules.Tasks.DTOs;

namespace TaskManager.Core.Modules.Tasks.Validators
{
    public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("DueDate must be in the future.");
        }
    }
}
