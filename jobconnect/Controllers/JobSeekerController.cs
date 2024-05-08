using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using jobconnect.Dtos;
using jobconnect.Models;
using jobconnect.Data;

//JobSeekerController about Job Seekers by Radwa Khaled

namespace jobconnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerController : Controller
    {
        private readonly IDataRepository<User> _userRepository;
        private readonly IDataRepository<Job> _jobRepository;
        private readonly IDataRepository<SavedJobs> _savedJobsRepository;

        public JobSeekerController(IDataRepository<User> userRepository, IDataRepository<Job> jobRepository,IDataRepository<SavedJobs> savedJobsRepository)
        {
            _userRepository = userRepository;
            _jobRepository = jobRepository;
            _savedJobsRepository = savedJobsRepository;

        }
        /*********************************************************** Getalljobs **********************************************************/
        [HttpGet("Getalljobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobRepository.GetAllAsync();
            var jobDtos = new List<JobDto>();
            //loop in job table
            foreach (var job in jobs)
            {
                //check id Accepted_by_admin to view
                var employer = await _userRepository.GetByIdAsync(job.EmpId);
                if (employer != null && job.Accepted_by_admin) 
                {
                    jobDtos.Add(new JobDto
                    {
                        Job_title = job.Job_title,
                        Job_description = job.Job_description,
                        Job_type = job.Job_type,
                        Post_creation_date = job.Post_creation_date,
                        location = job.location,
                        industry = job.industry,
                        salary_budget = job.salary_budget,
                        No_of_position_required = job.No_of_position_required,
                        EmpId = job.EmpId,
                        EmployerName = employer.Username,
                        Accepted_by_admin = job.Accepted_by_admin 
                    });
                }
            }

            return Ok(jobDtos);
        }


     /*********************************************************** SaveJob **********************************************************/

        [HttpPost("SaveJob")]
        public async Task<IActionResult> SaveJob(SaveJobDto saveJobDto)
        {
                            
                var jobSeeker = await _userRepository.GetByIdAsync(saveJobDto.JobSeekerId);
                var job = await _jobRepository.GetByIdAsync(saveJobDto.JobId);

                if (jobSeeker == null || job == null)
                {
                    return NotFound("User or Job not found");
                }

                var savedJob = new SavedJobs
                {
                    JobSeekerId = saveJobDto.JobSeekerId,
                    JobId = saveJobDto.JobId
                };

                
                await _savedJobsRepository.AddAsync(savedJob);
                await _savedJobsRepository.Save();

                return Ok("Job saved successfully");
            
           

        }




    }
}

