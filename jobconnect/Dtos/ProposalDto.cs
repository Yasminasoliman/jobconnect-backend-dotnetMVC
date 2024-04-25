using System.ComponentModel.DataAnnotations;

namespace jobconnect.Dtos
{
    public class ProposalDto
    {
        [Required]
        public string Proposal_date { get; set; }

        public string? brief_description { get; set; }

        [Required]
        public string CV_file { get; set; }
    }
}
