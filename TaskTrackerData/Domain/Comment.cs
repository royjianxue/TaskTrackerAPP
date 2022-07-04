using System.ComponentModel.DataAnnotations;


namespace TaskTrackerData.Domain
{
    public class Comment
    {
        public int commentId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Body { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime UpdatedOn { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
