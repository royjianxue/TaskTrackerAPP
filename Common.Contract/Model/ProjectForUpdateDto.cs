

namespace Common.Contract.Model
{
    public class ProjectForUpdateDto : DateTimeDto
    {
        public bool StatusCode { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }

    }
}
