using Common.Contract.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTrackerData.Domain
{
    public class Project : DateTimeDto
    {
        public Project()
        {
            Comments = new List<Comment>();
        }
        public int ProjectId { get; set; }
 
        public bool StatusCode { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }

        public int UserId { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
