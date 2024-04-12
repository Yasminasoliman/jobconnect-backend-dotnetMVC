namespace jobconnect.Models
{
    public class Proposal
    {
        public Guid Id { get; set; }

        public string Proposal_date { get; set; }

        public string brief_description { get; set; }

        public string CV_file { get; set; }

        //seeker_id
        public JobSeeker seeker_obj { get; set; }

        //job_id
        public Job job_obj { get; set; }

    }
}
