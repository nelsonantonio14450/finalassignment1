using System.ComponentModel.DataAnnotations;

namespace TodoAppWithJWT.Models.DTO.Request
{
    public class UserRegistrationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
    }
}