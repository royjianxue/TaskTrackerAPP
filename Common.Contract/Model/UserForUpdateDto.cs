

using System.ComponentModel.DataAnnotations;

namespace Common.Contract.Model
{
    public class UserForUpdateDto : DateTimeDto
    {

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? EmailAddress { get; set; }

        public string? Password { get; set; }


    }
}
