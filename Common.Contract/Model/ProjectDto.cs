

namespace Common.Contract.Model
{
    public class ProjectDto : DateTimeDto
    {

        public int ProjectId { get; set; }

        public bool StatusCode { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }

    }
}
