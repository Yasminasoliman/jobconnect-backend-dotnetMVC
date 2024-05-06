using jobconnect.Data;
using jobconnect.Dtos;
using jobconnect.Models;
using Microsoft.AspNetCore.Mvc;

//EmployerController about employers AND Post new jobs  posts by Radwa Khaled
namespace jobconnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : Controller
    {
        private readonly IDataRepository<Job> _jobRepository;

        public EmployerController(IDataRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }
  /*********************************************************** -CreateJobPost **********************************************************/
        [HttpPost("CreateJobPost")]
        public async Task<IActionResult> PostJob(JobDto jobDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
         
            var job = new Job
            {
                Job_title = jobDto.Job_title,
                Job_description = jobDto.Job_description,
                Job_type = jobDto.Job_type,
                location = jobDto.location,
                industry = jobDto.industry,
                salary_budget = jobDto.salary_budget,
                No_of_proposal_submitted = 0,
                No_of_position_required = jobDto.No_of_position_required,
                Accepted_by_admin = false,
                EmpId = jobDto.EmpId 
            };

            //current date and time
            job.Post_creation_date = DateTime.Now.ToString();

                        
                await _jobRepository.AddAsync(job);
                await _jobRepository.Save();

                return Ok(job);
        }

 /*********************************************************** review posts**********************************************************/

    }
}




