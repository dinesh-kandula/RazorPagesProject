using System.ComponentModel.DataAnnotations;

namespace TodoApplicationRazorPages.Model
{
    public enum Status
    {
        TODO, WORKING, DONE
    }

    public enum Priority
    {
        LOW, MEDIUM, HIGH
    }


    public class Todo
    {
        [Key]
        public Guid TodoId { get; set; }

        [Required, StringLength(120, MinimumLength = 3)]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
