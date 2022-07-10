

namespace Common.Contract.Model
{
    public class CommentDto : DateTimeDto
    {
        public int commentId { get; set; }
        public string? Title { get; set; }

        public string? Body { get; set; }

        public int ProjectId { get; set; }
    }
}
