using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<CreateIssueDTO> _validator;
        public IssueService(IUnitOfWork unit,IMapper mapper, IValidator<CreateIssueDTO> validator)
        {
            _unit = unit;   
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<IssueDTO> CreateIssueAsync(CreateIssueDTO issue)
        {   
            var validationResult = await _validator.ValidateAsync(issue);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
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
