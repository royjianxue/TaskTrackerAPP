using Common.Contract.Model;
using System.ComponentModel.DataAnnotations;


namespace TaskTrackerData.Domain
{
    public class Comment : DateTimeDto
    {
        public int commentId { get; set; }
   
        public string? Title { get; set; }

        public string? Body { get; set; }
   
        public int ProjectId { get; set; }
       
    }
}
