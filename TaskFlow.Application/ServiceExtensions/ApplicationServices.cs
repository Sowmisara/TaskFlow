using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.IService;
using TaskFlow.Application.Mapper;
using TaskFlow.Application.Service;

namespace TaskFlow.Application.ServiceExtensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationExtensionService(this IServiceCollection _service)
        {
            _service.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(MappingProfile).Assembly);
            });
            _service.AddScoped<IIssueService,IssueService>();
            return _service;
        }
    }
}
