using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApplicationRazorPages.Model
{
    public class Credential
    {
        [Key]
        public int Id { get; set; }

        [Required, DisplayName("User Name"), StringLength(60, MinimumLength = 3)]
        public required string Username { get; set; }

        [Required, EmailAddress, DisplayName("Email Id")]
        public required string EmailId { get; set; }

        [Required, DataType(DataType.Password), Length(minimumLength: 4, maximumLength: 16)]
        public required string Password { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }

}

