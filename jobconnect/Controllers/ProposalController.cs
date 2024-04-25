using jobconnect.Data;
using jobconnect.Models;
using jobconnect.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jobconnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly IDataRepository<Proposal> _proposalRepository;

        public ProposalController(IDataRepository<Proposal> departmentRepository)
        {
            _proposalRepository = departmentRepository;

        }

        //[Authorize]
        [HttpPost("apply")] // localhost:7000/api/proposak/apply
        public async Task<IActionResult> CreateDepartment(ProposalDto proposaldto)
        {
            if (proposaldto == null)
            {
                return BadRequest();
            }

            var proposal = new Proposal()
            {
                
            };

            await _proposalRepository.AddAsync(proposal);
            await _proposalRepository.Save();

            return Ok();
        }
    }
}
