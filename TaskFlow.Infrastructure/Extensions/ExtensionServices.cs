using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTO.Auth;
using TaskFlow.Application.IRepository;
using TaskFlow.Application.IService;
using TaskFlow.Infrastructure.Repository;
using TaskFlow.Infrastructure.Service;

namespace TaskFlow.Infrastructure.Extensions
{
    public static class ExtensionServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            service.AddScoped<IProjectRepository, ProjectRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IIssueRepository, IssueRepository>();
            service.AddScoped<IAuthService, AuthService>();

            return service;
        }
    }
}
