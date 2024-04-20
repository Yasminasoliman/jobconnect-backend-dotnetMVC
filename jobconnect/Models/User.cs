using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobconnect.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string passwordHash { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        public string? first_name { get; set; }

        public string? last_name { get; set; }

        public string? image { get; set; }

        [Required]
        public string phone { get; set; }

        //[ForeignKey("Job")]
        //public int Saved_job_id1 { get; set; } // saved jobs1

        //[ForeignKey("Job")]
        //public int Saved_job_id2 { get; set; } // saved jobs2


        //[ForeignKey("Job")]
        //public int Saved_job_id3 { get; set; } // saved jobs3


        public ICollection<Job> Job { get; set; }  // list of jobs for a particular employee

        public ICollection<Proposal> Proposal { get; set; }  // list of proposals for a particular job seeker

        [Required]
        public string user_type { get; set; }
    }
}
