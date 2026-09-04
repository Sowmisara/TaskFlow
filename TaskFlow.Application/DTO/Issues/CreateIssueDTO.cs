using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Models;

namespace TaskFlow.Application.DTO.Issues
{
    public class CreateIssueDTO 
    {
        public string Title { get; set; }

        public string Description { get; set; }

       // public IssueStatus Status { get; set; }

        public IssuePriority Priority { get; set; } 

        public Guid ProjectId { get; set; }

        public Guid? AssignedToUserId { get; set; }

        public DateTime? DueDate { get; set; }
    }

    public class CreateIssueValidatorDTO : AbstractValidator<CreateIssueDTO>
    {
        public CreateIssueValidatorDTO()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(x => x.ProjectId).NotEmpty().NotNull();

           RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Priority must be a valid value.");

            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("ProjectId is required.");
        }
    }
}
