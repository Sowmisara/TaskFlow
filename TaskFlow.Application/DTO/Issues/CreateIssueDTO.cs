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

        public Project Project { get; set; }

        public Guid? AssignedToUserId { get; set; }

        public User AssignedToUser { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
