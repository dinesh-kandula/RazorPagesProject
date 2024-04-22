using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoWebApplication.Model
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public string? Description { get; set; }

        public string? Author { get; set; }

        [DisplayName("Cover Picture")]
        public string? BookCover { get; set; }

        [NotMapped]
        public IFormFile? CoverImageFile { get; set; }

        // Many to Many 
        public ICollection<FavBook> FavBooks { get; set; }

    }
}
