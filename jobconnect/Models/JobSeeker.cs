namespace jobconnect.Models
{
    public class JobSeeker
    {
        public int Id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public String email { get; set; }

        public String first_name { get; set; }

        public String last_name { get; set; }

        public String image { get; set; } //To Do : check data type

        public String phone { get; set; } //To Do : check readonly or not
  
    }
}
