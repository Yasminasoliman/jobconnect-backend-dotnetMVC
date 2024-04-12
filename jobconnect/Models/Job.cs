namespace jobconnect.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Job_title { get; set; }
        public string Job_description { get; set;}

        public string Job_type { get; set;} // part, full , remote

        public string Post_creation_date { get; set; }

        public string location { get; set; } 

        public string industry { get; set; }

        public string salary_budget { get; set; }

        public int No_of_proposal_submitted { get; set; } 

        public bool Accepted_by_admin { get; set; }

        public Employeer emp_obj { get; set; }




    }
}
