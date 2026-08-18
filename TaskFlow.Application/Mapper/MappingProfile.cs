using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTO.Issues;
using TaskFlow.Domain.Models;

namespace TaskFlow.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateIssueDTO,Issue>();
            CreateMap<Issue,IssueDTO>();
        }
    }
}
