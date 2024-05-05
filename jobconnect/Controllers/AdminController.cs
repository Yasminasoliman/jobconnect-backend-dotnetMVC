using jobconnect.Data;
using jobconnect.Dtos;
using jobconnect.Models;
using Microsoft.AspNetCore.Mvc;


//AdminController about Manage employers (CRUD) AND Accept or refuse job posts by Radwa Khaled

namespace jobconnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IDataRepository<User> _userRepository;
        private readonly IDataRepository<Employer> _employerRepository;
        private readonly IDataRepository<Job> _jobRepository;
        public AdminController(IDataRepository<User> userRepository, IDataRepository<Employer> employerRepository, IDataRepository<Job> jobRepository)
        {
            _userRepository = userRepository;
            _employerRepository = employerRepository;
            _jobRepository = jobRepository;
        }
  /*********************************************************** CreateEmployer **********************************************************/

        [HttpPost("CreateEmployer")]
        public async Task<IActionResult> CreateEmployer(EmployerDto employerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(employerDto.Password, out passwordHash, out passwordSalt);


            var user = new User
            {
                Username = employerDto.Username,
                Email = employerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserType = "Employer"
            };


            var employer = new Employer
            {
                Company_name = employerDto.Company_name,
                Company_description = employerDto.Company_description,
                mainaddress = employerDto.mainaddress,
                User = user
            };

            // add user and employer to the database
            await _userRepository.AddAsync(user);
            await _employerRepository.AddAsync(employer);

            //dont forget the save function again Radwaaaaaaaa
            await _employerRepository.Save();

            return Ok($"Employer created successfully. Username: {user.Username} , Password: {employerDto.Password}");
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

  /*********************************************************** GetAllEmployers **********************************************************/
        [HttpGet("GetAllEmployers")]
        public async Task<IActionResult> GetAllEmployers()
        {
            var employers = await _employerRepository.GetAllAsync();
            return Ok(employers);
        }

  /*********************************************************** GetEmployerById **********************************************************/
        [HttpGet("GetEmployerById/{id}")]
        public async Task<IActionResult> GetEmployerById(int id)
        {
            var employer = await _employerRepository.GetByIdAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            return Ok(employer);
        }
  /*********************************************************** UpdateEmployerById **********************************************************/

        [HttpPut("UpdateEmployer/{id}")]
        public async Task<IActionResult> UpdateEmployer(int id, EmployerDto employerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingEmployer = await _employerRepository.GetByIdAsync(id);
            if (existingEmployer == null)
            {
                return NotFound();
            }

        
            existingEmployer.Company_name = employerDto.Company_name;
            existingEmployer.Company_description = employerDto.Company_description;
            existingEmployer.mainaddress = employerDto.mainaddress;

         
                // update employer in the database
                await _employerRepository.UpdateAsync(existingEmployer);
                await _employerRepository.Save();

                return Ok("Employer updated successfully.");
        }
  /*********************************************************** DeleteEmployerById **********************************************************/

  
        [HttpDelete("DeleteEmployer/{id}")]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            var existingEmployer = await _employerRepository.GetByIdAsync(id);
            if (existingEmployer == null)
            {
                return NotFound();
            }

     
                // delete employer from the database
                await _employerRepository.DeleteAsync(existingEmployer);
                await _employerRepository.Save();

                return Ok("Employer deleted successfully.");
        }

  /*********************************************************** Accept job posts  **********************************************************/

        [HttpPost("accept-job/{jobId}")]
        public async Task<IActionResult> AcceptJob(int jobId)
        {

                var job = await _jobRepository.GetByIdAsync(jobId);
                if (job == null)
                {
                    return NotFound("Job not found");
                }

                job.Accepted_by_admin = true;
                await _jobRepository.UpdateAsync(job);
                await _jobRepository.Save();

                return Ok("Job accepted successfully");
           

        }
 /*********************************************************** refuse job posts  **********************************************************/

        [HttpPost("refuse-job/{jobId}")]
        public async Task<IActionResult> RefuseJob(int jobId)
        {

                var job = await _jobRepository.GetByIdAsync(jobId);
                if (job == null)
                {
                    return NotFound("Job not found");
                }

                job.Accepted_by_admin = false;
                await _jobRepository.UpdateAsync(job);
                await _jobRepository.Save();

                return Ok("Job refused successfully");
        }




    }
}


