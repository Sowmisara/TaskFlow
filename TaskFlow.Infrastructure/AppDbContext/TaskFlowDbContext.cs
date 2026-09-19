using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Models;
using TaskFlow.Infrastructure.Identity;

namespace TaskFlow.Infrastructure.AppDbContext
{
    public class TaskFlowDbContext : IdentityDbContext<ApplicationUser>
    {
        public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
        {

        }


        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(TaskFlowDbContext).Assembly);
        }
    }

    
}
