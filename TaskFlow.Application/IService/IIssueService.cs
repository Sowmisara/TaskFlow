using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTO.Issues;

namespace TaskFlow.Application.IService
{
    public  interface IIssueService
    {
        Task<IssueDTO> CreateIssueAsync(CreateIssueDTO issue);

        Task<IEnumerable<IssueDTO>> GetAllIssueAsync();

        Task<IssueDTO> GetIssueByIdAsync(Guid id);
    }
}
