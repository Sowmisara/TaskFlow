using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTO.Issues;
using TaskFlow.Application.IRepository;
using TaskFlow.Application.IService;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Models;

namespace TaskFlow.Application.Service
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public IssueService(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;   
            _mapper = mapper;
        }
        public async Task<IssueDTO> CreateIssueAsync(CreateIssueDTO issue)
        {            
            var dto = _mapper.Map<Issue>(issue);
            dto.Status = IssueStatus.Open;
            var entity = await _unit.Issue.AddAsync(dto);
            await _unit.SaveAsync();
            return _mapper.Map<IssueDTO>(entity);
        }

        public async Task<IEnumerable<IssueDTO>> GetAllIssueAsync()
        {
            var entity = await _unit.Issue.GetAllAsync();
            return _mapper.Map<IEnumerable<IssueDTO>>(entity);
        }

        public async Task<IssueDTO> GetIssueByIdAsync(Guid id)
        {
            var entity = await _unit.Issue.GetByIdAsync(id);
            if(entity != null)
            {
                var dto = _mapper.Map<IssueDTO>(entity);
                return dto;
            }
            return null;

        }
    }
}
