using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.IRepository
{
    public interface IUnitOfWork
    {
        public IProjectRepository Project { get; init; }
        public IUserRepository User { get; init; }
        public IIssueRepository Issue { get; init; }

        public Task SaveAsync();
    }
}
