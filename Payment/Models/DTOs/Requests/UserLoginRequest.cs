using System.ComponentModel.DataAnnotations;

namespace PaymentWithJWT.Models.DTO.Request
{
    public class userLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}