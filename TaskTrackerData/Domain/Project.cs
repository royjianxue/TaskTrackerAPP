using System.ComponentModel.DataAnnotations;


namespace TaskTrackerData.Domain
{
    public class Project
    {
        public Project()
        {
            Comments = new List<Comment>();
        }
        public int ProjectId { get; set; }
 
        public bool StatusCode { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
