using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Mapper;

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
            return _service;
        }
    }
}
