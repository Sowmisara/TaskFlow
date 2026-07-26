using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.IRepository;
using TaskFlow.Domain.Models;
using TaskFlow.Infrastructure.AppDbContext;

namespace TaskFlow.Infrastructure.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskFlowDbContext context) : base(context)
        {

        }

      
    }
}
