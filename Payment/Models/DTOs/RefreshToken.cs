using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TodoAppWithJWT.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool isRevorked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpireDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}