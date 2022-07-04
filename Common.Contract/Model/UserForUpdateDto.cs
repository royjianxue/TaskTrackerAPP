

using System.ComponentModel.DataAnnotations;

namespace Common.Contract.Model
{
    public class UserForUpdateDto
    {
        [Required(ErrorMessage = "This data field is requried!")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "This data field is requried!")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "This data field is requried!")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "This data field is requried!")]
        public string? Password { get; set; }


    }
}
