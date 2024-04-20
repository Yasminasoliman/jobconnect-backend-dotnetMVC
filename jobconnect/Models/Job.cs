using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobconnect.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Job_title { get; set; }
        public string Job_description { get; set;}

        [Required]
        public string Job_type { get; set;} // A field to set if job is (part time, full time,or remote)

        [Required]
        public string Post_creation_date { get; set; }

        [Required]
        public string location { get; set; }

        [Required]
        public string industry { get; set; }

        [Required]
        public string salary_budget { get; set; }

        public int? No_of_proposal_submitted { get; set; }

        [Required]
        public int No_of_position_required { get; set; }

        [Required]
        public bool Accepted_by_admin { get; set; } = false;

        
        [ForeignKey("User")]
        public int UserId { get; set; } // Employee_id
        public User User { get; set; } // navigation to employee (owner of job)

        public ICollection<Proposal> Proposal { get; set; }  // list of proposals for a particular job


    }
}
