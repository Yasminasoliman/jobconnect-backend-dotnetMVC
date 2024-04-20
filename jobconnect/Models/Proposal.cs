using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobconnect.Models
{
    public class Proposal
    {
        //UserId is ForeignKey
        //JobId is ForeignKey
        //UserId + JobId are composite primary key
        // This relation is set on DataContext class
        public int UserId { get; set; }
        public User? User { get; set; } //navigation to jobseeker (The person that applied for the job)

        public int JobId { get; set; }
        public Job? Job { get; set; } //navigation to Job (That is applied for)

        [Required]
        public string Proposal_date { get; set; }

        public string? brief_description { get; set; }

        [Required]
        public string CV_file { get; set; }

    }
}
