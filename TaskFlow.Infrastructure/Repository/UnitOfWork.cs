using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.IRepository;
using TaskFlow.Infrastructure.AppDbContext;

namespace TaskFlow.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProjectRepository Project { get; init; }
        public IUserRepository User { get; init; }
        public IIssueRepository Issue { get; init; }

        private readonly TaskFlowDbContext _dbContext;
        public UnitOfWork(TaskFlowDbContext dbContext,IProjectRepository projectRepository, IUserRepository userRepository, IIssueRepository issueRepository)
        {
            _dbContext = dbContext;
            Project = projectRepository;
            User = userRepository;
            Issue = issueRepository;
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
