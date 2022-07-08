using Common.Contract.Model;
using System.ComponentModel.DataAnnotations;


namespace TaskTrackerData.Domain
{
    public class Comment : DateTimeDto
    {
        public int commentId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Body { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
