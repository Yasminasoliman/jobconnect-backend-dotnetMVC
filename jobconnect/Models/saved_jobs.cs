namespace jobconnect.Models
{
    public class saved_jobs
    {

        public int id { get; set; }

        public Job job_obj { get; set; }

        public JobSeeker seeker_obj { get; set; }

        public String saved_date { get; set; }


    }
}
