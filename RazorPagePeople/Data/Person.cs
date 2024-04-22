using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace RazorPagePeople.Data
{
    public enum Gender {
        MALE, FEMALE, OTHER
    }

    public enum City
    {
        Hyderabad, 
        Chennai,
        Bengaluru,
        Kochi,
        Pune,
        Noida,
        Delhi, Kolkata, Vishakapatanam, Mumbai, Harayana, 
    }

    public enum Role
    {
        ADMIN,
        HERO,
        SHIELD,
        OTHERS
    }

    public class Person
    {
        public int Id { get; set; }

        [Required,StringLength(60, MinimumLength = 3)]
        public required string UserName { get; set; }

        [Required, StringLength(60, MinimumLength = 3)]
        public required string Name { get; set; }

        [Required, EmailAddress, DisplayName("Email ID")]
        public string? EmailId { get; set; }

        [DataType(DataType.Password), Length(minimumLength: 4, maximumLength: 16)]
        public required string Password { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public Role Role { get; set; }

        public City City { get; set; }

        [DisplayName("Profile Picture")]
        public string? Profile { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
