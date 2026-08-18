using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTO.Issues;
using TaskFlow.Application.IRepository;
using TaskFlow.Application.IService;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _service;
        public IssueController(IIssueService service) 
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateIssue(CreateIssueDTO issue)
        {
            var entity = await _service.CreateIssueAsync(issue);
            return Ok(entity);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllIssue()
        {
            var entity = await _service.GetAllIssueAsync();
            return Ok(entity);
        }

        [HttpGet("{id}")] //both route and http method
        //[Route("GetSpecificIssue")] //only for route
        public async Task<ActionResult> GetSpecificIssue(Guid id)
        {
            var entity = await _service.GetIssueByIdAsync(id);
            return Ok(entity);
        }
    }
}
